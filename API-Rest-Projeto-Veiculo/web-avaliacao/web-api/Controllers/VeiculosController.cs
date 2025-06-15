using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace web_api.Controllers
{
    public class VeiculosController : ApiController
    {
        readonly Repositories.Veiculo repository;
        readonly Utils.Logger logger;

        public VeiculosController()
        {
            repository = new Repositories.Veiculo(Configurations.Config.GetConnection());
            logger = new Utils.Logger(Configurations.Config.GetLogPath());
        }

        // GET: api/Veiculos
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await repository.GetAll());
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            } 
        }

        //GET: api/Veiculos/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Models.Veiculo veiculo = await repository.GetById(id);

                if (veiculo.Id == 0)
                    return NotFound();
                return Ok(veiculo);
                
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        //GET: api/Veiculos/nome
        [Route("api/Veiculos/{nome:alpha}")]
        public async Task<IHttpActionResult> Get(string nome)
        {
            if (nome.Length < 3)
                return BadRequest("Informe o mínimo de 3 caracteres no nome do veículo.");

            try
            {
                List<Models.Veiculo> veiculos = await repository.GetByName(nome);
                if (veiculos.Count == 0)
                    return NotFound();
                return Ok(veiculos);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // POST: api/Veiculos
        public async Task<IHttpActionResult> Post([FromBody] Models.Veiculo veiculo)
        {
            if (veiculo == null)
                return BadRequest("Os dados enviados do veículo estão incorretos");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await repository.Add(veiculo);
                veiculo.OpcionalToString();
                return Ok(veiculo);
            }
            catch(Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // PUT: api/Veiculos/5
        public async Task<IHttpActionResult> Put(int id, [FromBody] Models.Veiculo veiculo )
        {
            if (veiculo == null)
                return BadRequest("Os dados do veículo não foram enviados corretamente!");

            if (veiculo.Id != id)
                return BadRequest("O id da rota não corresponde ao id do veículo!");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (!await repository.Update(veiculo))
                    return BadRequest();
                return Ok(veiculo);
            }
            catch(Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // DELETE: api/Veiculos/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                if (!await repository.Delete(id))
                    return NotFound();
                return Ok();
            }
            catch(Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }
    }
}
