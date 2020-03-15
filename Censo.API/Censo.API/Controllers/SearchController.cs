namespace Censo.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Interfaces.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [ApiController]
    [Route("/api/census/[controller]/[action]")]
    public class SearchController : ControllerBase
    {
        private readonly IAnswerRepository _repository;

        public SearchController(IAnswerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<SearchResultViewModel>> Filter(string name, NameComparisonEnum namecomparison, int? region = null, int? gender = null, int? ethnicity = null, int? schooling = null)
        {
            try
            {
                var (searchResult, total) = await _repository.ApplyFilterAsync(name, namecomparison, region, gender, ethnicity, schooling);
                return Ok(new SearchResultViewModel { Fraction = searchResult, Total = total });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IEnumerable<AnswerViewModel>>>> Genealogy(int id, int parentLevel = 0)
        {
            try
            {
                var result = await _repository.ApplyGenealogyFilter(id, parentLevel);
                return Ok(result.ToAnswerViewModel());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
