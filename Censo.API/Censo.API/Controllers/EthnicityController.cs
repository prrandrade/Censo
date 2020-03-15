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
    public class EthnicityController : ControllerBase
    {
        private readonly IEthnicityRepository _repository;

        public EthnicityController(IEthnicityRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtém a identificação de uma etnia dado seu código
        /// </summary>
        /// <param name="id">Código da etnia</param>
        /// <returns>Objeto com identificação e descrição da etnia</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EthnicityModel>> Get(int id)
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
        /// Obtém todas as etnias disponíveis para responder o censo
        /// </summary>
        /// <returns>Lista com todas as etnias e respectivos códigos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EthnicityModel>>> GetAll()
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
        /// <param name="value">Objeto com descrição da etnia</param>
        /// <returns>Objeto com identificação e descrição da etnia</returns>
        [HttpPost]
        public async Task<ActionResult<EthnicityModel>> Post([FromBody] EthnicityModel value)
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
        /// Atualiza a descrição de uma etnia
        /// </summary>
        /// <param name="value">Objeto com identificação e descrição da etnia</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] EthnicityModel value)
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