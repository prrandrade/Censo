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
    public class GenderController : ControllerBase
    {
        private readonly IGenderRepository _repository;

        public GenderController(IGenderRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtém a identificação de um gênero dado seu código
        /// </summary>
        /// <param name="id">Código do gênero</param>
        /// <returns>Objeto com identificação e descrição do gênero</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GenderModel>> Get(int id)
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
        /// Obtém todos os gêneros disponíveis para responder o censo
        /// </summary>
        /// <returns>Lista com todos os gêneros e respectivos códigos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenderModel>>> GetAll()
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
        /// Cadastra um gênero novo
        /// </summary>
        /// <param name="value">Objeto com descrição do gênero</param>
        /// <returns>Objeto com identificação e descrição do gênero</returns>
        [HttpPost]
        public async Task<ActionResult<RegionModel>> Post([FromBody] GenderModel value)
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
        /// Atualiza a descrição de um gênero
        /// </summary>
        /// <param name="value">Objeto com identificação e descrição do gênero</param>
        [HttpPut]
        public async Task<ActionResult> Put(GenderModel value)
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