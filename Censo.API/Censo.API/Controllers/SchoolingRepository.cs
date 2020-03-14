﻿namespace Censo.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Interfaces.Data;
    using Domain.Model;
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

        [HttpGet("{id}")]
        public async Task<SchoolingModel> Get(int id)
        {
            return await _repository.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<SchoolingModel>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult<SchoolingModel>> Post(SchoolingModel value)
        {
            var result = await _repository.CreateAsync(value);
            return CreatedAtAction(nameof(Get), new {id = result.Id}, result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(SchoolingModel value)
        {
            await _repository.UpdateAsync(value);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return Ok(result);
        }
    }
}