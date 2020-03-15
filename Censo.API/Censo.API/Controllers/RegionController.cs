namespace Censo.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Interfaces.Data;
    using Domain.Model;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/census/[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _repository;

        public RegionController(IRegionRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtém a identificação de uma região dado seu código
        /// </summary>
        /// <param name="id">Código da região</param>
        /// <returns>Objeto com identificação e descrição da região</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RegionModel>> Get(int id)
        {
            var result = await _repository.GetAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Obtém todas as regiões disponíveis para responder o censo
        /// </summary>
        /// <returns>Lista com todas as regiões e respectivos códigos</returns>
        [HttpGet]
        public async Task<IEnumerable<RegionModel>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Cadastra uma região nova
        /// </summary>
        /// <param name="value">Objeto com descrição da região</param>
        /// <returns>Objeto com identificação e descrição da região</returns>
        [HttpPost]
        public async Task<ActionResult<RegionModel>> Post([FromBody] RegionModel value)
        {
            var result = await _repository.CreateAsync(value);
            return CreatedAtAction(nameof(Get), new {id = result.Id}, result);
        }

        /// <summary>
        /// Atualiza a descrição de uma região
        /// </summary>
        /// <param name="value">Objeto com identificação e descrição da região</param>
        [HttpPut]
        public async Task<ActionResult> Put(RegionModel value)
        {
            await _repository.UpdateAsync(value);
            return Ok();
        }
    }
}
