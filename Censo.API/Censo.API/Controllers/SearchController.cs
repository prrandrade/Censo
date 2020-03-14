namespace Censo.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Interfaces.Data;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [ApiController]
    [Route("/api/census/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IAnswerRepository _repository;

        public SearchController(IAnswerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> Filter(string name, int? region = null, int? gender = null, int? ethnicity = null, int? schooling = null)
        {
            var (searchResult, total) = await _repository.ApplyFilterAsync(name, region, gender, ethnicity, schooling);

            return Ok(new
            {
                searchResult,
                total
            });
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<List<AnswerViewModel>>>> Genealogy(int id, int parentLevel = 0)
        {
            var result = await _repository.ApplyGenealogyFilter(id, parentLevel);
            return Ok(result.ToAnswerViewModel(false));
        }

    }
}
