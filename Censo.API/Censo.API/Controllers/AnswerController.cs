namespace Censo.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Interfaces.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [ApiController]
    [Route("/api/census/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository _repository;

        public AnswerController(IAnswerRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtém uma resposta de censo
        /// </summary>
        /// <param name="id">Identificação única da resposta</param>
        /// <returns>Objeto com dados da resposta</returns>
        /// <response code="200">Informação obtida com sucesso</response>
        /// <response code="404">Informação não foi encontrada</response>
        /// <response code="500">Erro an obter a informação</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerViewModel>> Get(int id)
        {
            try
            {
                var result = await _repository.GetAsync(id);
                if (result == null) return NotFound();
                return Ok(result.ToAnswerViewModel());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obtém todas as respostas
        /// </summary>
        /// <returns>Lista de todas as respostas enviadas</returns>
        /// <response code="200">Informação obtida com sucesso</response>
        /// <response code="500">Erro an obter a informação</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerViewModel>>> GetAll()
        {
            try
            {
                return Ok((await _repository.GetAllAsync()).ToAnswerViewModel(false));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Envia uma nova resposta do censo
        /// </summary>
        /// <param name="value">Objeto com as respostas</param>
        /// <returns>Resposta persistida em banco</returns>
        /// <response code="201">Informação persistida em banco corretamente</response>
        /// <response code="500">Erro an salvar a informação</response>
        [HttpPost]
        public async Task<ActionResult<AnswerViewModel>> Post([FromBody] AnswerViewModel value)
        {
            try
            {
                var census = value.ToAnswerModel();
                var parents = value.RetrieveAnswerModelParents();
                var children = value.RetrieveAnswerModelChildren();

                var result = await _repository.CreateWithParentsAndChidrenAsync(census, parents, children);
                return CreatedAtAction(nameof(Get), new {id = result.Id}, result.ToAnswerViewModel());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}