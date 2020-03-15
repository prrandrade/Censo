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

        /// <summary>
        /// Obtém dados estatísiticos de uma busca
        /// </summary>
        /// <param name="name">Nome ou sobrenome do usuário</param>
        /// <param name="namecomparison">Se o nome for passado, define o tipo de comparação (0 para início, 1 para qualquer parte, 2 para fim e 3 para igualidade)</param>
        /// <param name="region">Busca pelo código específico da região</param>
        /// <param name="gender">Busca pelo código específico do gênero</param>
        /// <param name="ethnicity">Busca pelo código específico da etnia</param>
        /// <param name="schooling">Busca pelo código específico de escolaridade</param>
        /// <returns>Objeto de busca com a contagem de respostas que se encaixam na busca e a contagem geral de respostas</returns>
        /// <response code="200">Informação obtida com sucesso</response>
        /// <response code="500">Erro an obter a informação</response>
        [HttpGet]
        public async Task<ActionResult<SearchResultViewModel>> Filter(string name, NameComparisonEnum? namecomparison, int? region = null, int? gender = null, int? ethnicity = null, int? schooling = null)
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

        /// <summary>
        /// Traz informações sobre a árvore genealógica de uma resposta
        /// </summary>
        /// <param name="id">Identificação da resposta</param>
        /// <param name="parentLevel">Nível máximo da árvore genealógica</param>
        /// <returns>Lista com nível para montagem da árvore genealogica, começando a partir da própria resposta</returns>
        /// <response code="200">Informação obtida com sucesso</response>
        /// <response code="500">Erro an obter a informação</response>
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
