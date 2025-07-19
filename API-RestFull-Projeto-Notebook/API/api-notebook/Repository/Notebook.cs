using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utils;
using Utils.Interface;

namespace Repository
{
    public class Notebook : IRepository<Models.Notebook>
    {
        readonly SqlConnection conn;
        readonly SqlCommand cmd;
        readonly ICacheService cacheService;
        readonly string keyCache;

        public int CacheExpirationTime { get; set; }
        public Notebook(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cacheService = new MemoryCacheService();
            keyCache = "NotebooksCache";
            CacheExpirationTime = 15;
        }


        public async Task<List<Models.Notebook>> GetAllAsync()
        {
            List<Models.Notebook> listaNotebooks;

            listaNotebooks = cacheService.Get<List<Models.Notebook>>(keyCache);

            if(listaNotebooks != null)
                return listaNotebooks;

            listaNotebooks = new List<Models.Notebook>();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Nome, Valor, Opcionais FROM notebook";
                    await cmd.ExecuteNonQueryAsync();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Models.Notebook notebook = new Models.Notebook();
                        await MapperNotebookToDrAsync(notebook, dr);
                        listaNotebooks.Add(notebook);
                    }
                }
                cacheService.Set(keyCache, listaNotebooks,CacheExpirationTime);
                return listaNotebooks;
            }
        }

        public async Task<Models.Notebook> GetByIdAsync(int id)
        {
            Models.Notebook notebook = new Models.Notebook();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Nome, Valor, Opcionais FROM notebook WHERE Id = @Id;";
                    cmd.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int)).Value = id;
                    await cmd.ExecuteNonQueryAsync();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                        await MapperNotebookToDrAsync(notebook, dr);
                }
                return notebook;
            }
        }

        public async Task<List<Models.Notebook>> GetByNameAsync(string name)
        {
            List<Models.Notebook> listaNotebooks = new List<Models.Notebook>();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Nome, Valor, Opcionais FROM notebook WHERE Nome Like @Nome;";
                    cmd.Parameters.Add(new SqlParameter("Nome", System.Data.SqlDbType.VarChar)).Value = $"%{name}%";
                    await cmd.ExecuteNonQueryAsync();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Models.Notebook notebook = new Models.Notebook();
                        await MapperNotebookToDrAsync(notebook, dr);
                        listaNotebooks.Add(notebook);
                    }
                }
                return listaNotebooks;
            }
        }

        public async Task AddAsync(Models.Notebook notebook)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "INSERT INTO notebook VALUES(@Nome, @Valor, @Opcionais); SELECT scope_identity() FROM notebook";
                    await MapperNotebookToParamtersAsync(notebook);
                    notebook.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    cacheService.Remove(keyCache);
                }

            }
        }

        public async Task<bool> UpdateAsync(Models.Notebook notebook)
        {
            int linhasAfetadas;
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "UPDATE notebook SET Nome = @Nome, Valor = @Valor, Opcionais = @Opcionais WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int)).Value = notebook.Id;
                    await MapperNotebookToParamtersAsync(notebook);
                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
                if (linhasAfetadas == 0)
                    return false;
                cacheService.Remove(keyCache);
                return true;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            int linhasAfetadas;

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "DELETE notebook WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.Int)).Value = id;
                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
                if (linhasAfetadas == 0)
                    return false;
                cacheService.Remove(keyCache);
                return true;
            }
        }
        private async Task MapperNotebookToDrAsync(Models.Notebook notebook, SqlDataReader dr)
        {
            notebook.Id = (int)dr["id"];
            notebook.Nome = dr["Nome"].ToString();
            notebook.Valor = Convert.ToDouble(dr["Valor"]);
            notebook.Opcionais = dr["Opcionais"].ToString();
        }
        private async Task MapperNotebookToParamtersAsync(Models.Notebook notebook)
        {
            cmd.Parameters.Add(new SqlParameter("Nome", System.Data.SqlDbType.VarChar)).Value = notebook.Nome;
            cmd.Parameters.Add(new SqlParameter("Valor", System.Data.SqlDbType.Decimal)).Value = notebook.Valor;
            cmd.Parameters.Add(new SqlParameter("@Opcionais", SqlDbType.VarChar)).Value = notebook.Opcionais == "" ? (object)DBNull.Value : notebook.Opcionais;
        }
    }
}
