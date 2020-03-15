﻿namespace Censo.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Interfaces.Data;
    using Domain.Model;
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
            var result = await _repository.GetAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Obtém todos os gêneros disponíveis para responder o censo
        /// </summary>
        /// <returns>Lista com todos os gêneros e respectivos códigos</returns>
        [HttpGet]
        public async Task<IEnumerable<GenderModel>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Cadastra um gênero novo
        /// </summary>
        /// <param name="value">Objeto com descrição do gênero</param>
        /// <returns>Objeto com identificação e descrição do gênero</returns>
        [HttpPost]
        public async Task<ActionResult<RegionModel>> Post([FromBody] GenderModel value)
        {
            var result = await _repository.CreateAsync(value);
            return CreatedAtAction(nameof(Get), new {id = result.Id}, result);
        }

        /// <summary>
        /// Atualiza a descrição de um gênero
        /// </summary>
        /// <param name="value">Objeto com identificação e descrição do gênero</param>
        [HttpPut]
        public async Task<ActionResult> Put(GenderModel value)
        {
            await _repository.UpdateAsync(value);
            return Ok();
        }
    }
}