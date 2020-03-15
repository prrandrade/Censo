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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerViewModel>>> GetAll()
        {
            try
            {
                return Ok((await _repository.GetAllAsync()).ToAnswerViewModel());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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