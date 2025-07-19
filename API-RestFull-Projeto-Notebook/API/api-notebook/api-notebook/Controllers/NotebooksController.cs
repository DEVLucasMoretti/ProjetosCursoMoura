using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.Routing;

namespace api_notebook.Controllers
{
    public class NotebooksController : ApiController
    {
        Utils.Logger logger;
        Repository.Notebook repository;
        public NotebooksController()
        {
            logger = new Utils.Logger(Configurations.Config.GetLogPath());
            repository = new Repository.Notebook(Configurations.Config.GetConnection());
            repository.CacheExpirationTime = Configurations.Config.GetCacheExpiration("cacheExpirationTimeInSeconds");

        }
        // GET: api/Notebooks
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await repository.GetAllAsync());
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError(ex);
            }
        }

        // GET: api/Notebooks/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Models.Notebook notebook = await repository.GetByIdAsync(id);
                if (notebook.Id == 0)
                    return NotFound();
                return Ok(notebook);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError(ex);
            }
        }

        // GET: api/Notebooks/5
        [Route("api/Notebooks/{nome:alpha}")]
        public async Task<IHttpActionResult> Get(string nome)
        {
            try
            {
                List<Models.Notebook> listaNotebook = await repository.GetByNameAsync(nome);
                if (listaNotebook.Count() == 0)
                    return NotFound();
                return Ok(listaNotebook);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError(ex);
            }
        }

        // POST: api/Notebooks
        public async Task<IHttpActionResult> Post([FromBody]Models.Notebook notebook)
        {
            if (notebook == null)
                return BadRequest("O notebook precisa estar preenchido corretamente.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                notebook.OpcionalToString();
                await repository.AddAsync(notebook);
                if (notebook.Id == 0)
                    return BadRequest();
                return Ok(notebook);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError(ex);
            }
        }

        // PUT: api/Notebooks/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Models.Notebook notebook)
        {
            if (notebook == null)
                return BadRequest("O notebook precisa estar preenchido corretamente.");
            if (notebook.Id != id)
                return BadRequest("id do notebook não correponde ao id da rota.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                notebook.OpcionalToString();
                if(!await repository.UpdateAsync(notebook))
                    return BadRequest();
                return Ok(notebook);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Notebooks/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                if(!await  repository.DeleteAsync(id))
                    return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError(ex);
            }
        }
    }
}
