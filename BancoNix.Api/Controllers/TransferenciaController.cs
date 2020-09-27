using BancoNix.Aplicacao;
using BancoNix.Aplicacao.Commands;
using BancoNix.Aplicacao.Filtros;
using BancoNix.Aplicacao.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BancoNix.Api.Controllers
{
    [Route("api/transferencias")]
    [ApiController]
    public class TransferenciaController : ControllerBase
    {
        private readonly ITransferenciaService _transferenciaService;

        public TransferenciaController(ITransferenciaService transferenciaService)
        {
            _transferenciaService = transferenciaService;
        }

        /// <summary>
        /// Listar transferências não removidas
        /// </summary>
        /// <param name="filtro">FiltroTransferencia opcional</param>
        /// <returns>ResultadoTransferenciaModel</returns>

        // GET: api/<TransferenciaController>
        [HttpGet]
        public async Task<ActionResult<ResultadoTransferenciaModel>> Get([FromQuery]FiltroTransferencia filtro)
        {
            return Ok(await _transferenciaService.Buscar(filtro));
        }

        /// <summary>
        /// Busca transferência por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")] // busca por id
        public async Task<ActionResult<TransferenciaModel>> Get(int id)
        {
            return Ok(await _transferenciaService.Buscar(id));
        }

        /// <summary>
        /// Insere uma transferência
        /// </summary>
        /// <param name="comando">Comando para inserir uma transferência</param>
        /// <returns>Retorna sucesso ou falha</returns>
        [HttpPost]
        public async Task<ActionResult<TransferenciaModel>> Post([FromBody] CriarTransferenciaCommand comando)
        {
            var inseriu = await _transferenciaService.Criar(comando);

            if (inseriu)
                return Accepted();
            
            return BadRequest("Não foi possível inserir a transferência");
        }

        /// <summary>
        /// Remove uma transferência
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna sucesso ou falha</returns>
        // DELETE api/<TransferenciaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Accepted(await _transferenciaService.Remover(id));
        }
    }
}
