namespace Censo.Domain.Interfaces.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;

    public interface IAnswerRepository : IRepository<AnswerModel>
    {
        Task<AnswerModel> CreateWithParentsAndChidrenAsync(AnswerModel model, IEnumerable<AnswerModel> parents, IEnumerable<AnswerModel> children);

        Task<(int searchResult, int total)> ApplyFilterAsync(string name, NameComparisonEnum nameComparison, int? region, int? gender, int? ethnicity, int? schooling);

        Task<List<List<AnswerModel>>> ApplyGenealogyFilter(int id, int parentMaxLevel = 0);

    }
}
