namespace Censo.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Interfaces.Data;
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
        public async Task<AnswerViewModel> Get(int id)
        {
            return (await _repository.GetAsync(id)).ToAnswerViewModel();
        }

        [HttpGet]
        public async Task<IEnumerable<AnswerViewModel>> GetAll()
        {
            return (await _repository.GetAllAsync()).ToAnswerViewModel();
        }

        [HttpPost]
        public async Task<ActionResult<AnswerViewModel>> Post([FromBody] AnswerViewModel value)
        {
            var census = value.ToAnswerModel();
            var parents = value.RetrieveAnswerModelParents();
            var children = value.RetrieveAnswerModelChildren();

            var result = await _repository.CreateWithParentsAndChidrenAsync(census, parents, children);
            return CreatedAtAction(nameof(Get), new {id = result.Id}, result.ToAnswerViewModel());
        }
    }
}