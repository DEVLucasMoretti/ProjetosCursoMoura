using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using web_api.Models;
namespace web_api.Repositories
{
    public class Veiculo
    {
        readonly SqlConnection conn;
        readonly SqlCommand cmd;

        public Veiculo(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        public async Task<List<Models.Veiculo>> GetAll()
        {
            List<Models.Veiculo> veiculos = new List<Models.Veiculo>();

            using (conn)
            {
                await conn.OpenAsync();

                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Marca, Nome, AnoModelo, DataFabricacao, Valor, Opcionais FROM Veiculos";
                    
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    while (await dr.ReadAsync())
                    {
                        Models.Veiculo veiculo = new Models.Veiculo();
                        MapperDrToVeiculo(veiculo,dr);
                        veiculos.Add(veiculo);
                    }
                }
            }
            return veiculos;
        }

        public async Task<Models.Veiculo> GetById(int id)
        {
            Models.Veiculo veiculo = new Models.Veiculo();

            using (conn)
            {
                await conn.OpenAsync();

                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Marca, Nome, AnoModelo, DataFabricacao, Valor, Opcionais FROM Veiculos WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("@Id",SqlDbType.Int)).Value = id;

                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    if (await dr.ReadAsync())
                        MapperDrToVeiculo(veiculo,dr);
                }
            }
            return veiculo;
        }

        public async Task<List<Models.Veiculo>> GetByName(string nome)
        {
            List<Models.Veiculo> veiculos = new List<Models.Veiculo>();

            using (conn)
            {
                await conn.OpenAsync();

                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Marca, Nome, AnoModelo, DataFabricacao, Valor, Opcionais FROM Veiculos WHERE Nome LIKE @Nome";
                    cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = $"%{nome}%";

                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    while (await dr.ReadAsync())
                    {
                        Models.Veiculo veiculo = new Models.Veiculo();
                        MapperDrToVeiculo(veiculo, dr);
                        veiculos.Add(veiculo);
                    }
                }
            }
            return veiculos;
        }

        public async Task Add(Models.Veiculo veiculo)
        {
            using (conn)
            {
                await conn.OpenAsync();

                using (cmd)
                {
                    cmd.CommandText = "INSERT INTO Veiculos values(@Marca, @Nome, @AnoModelo, @DataFabricacao, @Valor, @Opcionais); SELECT scope_identity();";
                    MapperVeiculoToParameter(veiculo);
                    veiculo.Id =  Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }
        }

        public async Task<bool> Update(Models.Veiculo veiculo)
        {
            int linhasAfetadas;
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "UPDATE Veiculos SET Marca = @Marca, Nome = @Nome, AnoModelo = @AnoModelo, DataFabricacao = @DataFabricacao, Valor = @Valor, Opcionais = @Opcionais WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = veiculo.Id;
                    MapperVeiculoToParameter(veiculo);

                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }
            return linhasAfetadas == 1;
        }

        public async Task<bool> Delete(int id)
        {
            int linhasAfetadas;
            using (conn)
            {
                await conn.OpenAsync();
                using(cmd)
                {
                    cmd.CommandText = "DELETE FROM Veiculos WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("@Id",SqlDbType.Int)).Value = id;
                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }
            return linhasAfetadas == 1;
        }

        private  void MapperDrToVeiculo(Models.Veiculo veiculo, SqlDataReader dr)
        {
            veiculo.Id = (int)dr["Id"];
            veiculo.Marca = dr["Marca"].ToString();
            veiculo.Nome = dr["Nome"].ToString();
            veiculo.AnoModelo = (int)dr["AnoModelo"];
            veiculo.DataFabricacao = (DateTime)dr["DataFabricacao"];
            veiculo.Valor = Convert.ToDouble(dr["Valor"]);
            veiculo.Opcionais = dr["Opcionais"].ToString();
        }

        private void MapperVeiculoToParameter(Models.Veiculo veiculo)
        {
            cmd.Parameters.Add(new SqlParameter("@Marca", SqlDbType.VarChar)).Value = veiculo.Marca;
            cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = veiculo.Nome;
            cmd.Parameters.Add(new SqlParameter("@AnoModelo", SqlDbType.Int)).Value = veiculo.AnoModelo;
            cmd.Parameters.Add(new SqlParameter("@DataFabricacao", SqlDbType.Date)).Value = veiculo.DataFabricacao;
            cmd.Parameters.Add(new SqlParameter("@Valor", SqlDbType.Decimal)).Value = veiculo.Valor;
            cmd.Parameters.Add(new SqlParameter("@Opcionais", SqlDbType.VarChar)).Value = veiculo.Opcionais == null ? (object)DBNull.Value : veiculo.Opcionais;
            veiculo.Opcionais = veiculo.Opcionais.ToString();
        }
    }
}