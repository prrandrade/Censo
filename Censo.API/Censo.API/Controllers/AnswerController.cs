namespace Censo.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Interfaces.Data;
    using Domain.Model;
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
        public async Task<AnswerModel> Get(int id)
        {
            return await _repository.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<AnswerModel>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AnswerModel>> Post(CensusAnswerViewModel value)
        {
            var census = value.ToCensusAnswerModel();
            var parents = value.RetrieveCensusAnswerModelParents();
            var children = value.RetrieveCensusAnswerModelChildren();

            var result = await _repository.CreateWithParentsAndChidrenAsync(census, parents, children);
            return CreatedAtAction(nameof(Get), new {id = result.Id}, result);
        }

    }
}