namespace Censo.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Interfaces.Data;
    using Domain.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/census/[controller]")]
    public class SchoolingController : ControllerBase
    {
        private readonly ISchoolingRepository _repository;

        public SchoolingController(ISchoolingRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtém a identificação de um grau de escolaridade dado seu código
        /// </summary>
        /// <param name="id">Código do grau de escolaridade</param>
        /// <returns>Objeto com identificação e descrição do grau de escolaridade</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolingModel>> Get(int id)
        {
            try
            {
                var result = await _repository.GetAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obtém todos os grau de escolaridade disponíveis para responder o censo
        /// </summary>
        /// <returns>Lista com todos os grau de escolaridade e respectivos códigos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolingModel>>> GetAll()
        {
            try
            {
                return Ok(await _repository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Cadastra uma etnia nova
        /// </summary>
        /// <param name="value">Objeto com descrição do grau de escolaridade</param>
        /// <returns>Objeto com identificação e descrição do grau de escolaridade</returns>
        [HttpPost]
        public async Task<ActionResult<SchoolingModel>> Post([FromBody] SchoolingModel value)
        {
            try
            {
                var result = await _repository.CreateAsync(value);
                return CreatedAtAction(nameof(Get), new {id = result.Id}, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Atualiza a descrição de um grau de escolaridade
        /// </summary>
        /// <param name="value">Objeto com identificação e descrição do grau de escolaridade</param>
        [HttpPut]
        public async Task<ActionResult> Put(SchoolingModel value)
        {
            try
            {
                await _repository.UpdateAsync(value);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}