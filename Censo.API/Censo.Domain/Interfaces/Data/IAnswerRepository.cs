namespace Censo.Domain.Interfaces.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;

    public interface IAnswerRepository : IRepository<AnswerModel>
    {
        Task<AnswerModel> GetByNameAsync(string firstName, string lastName);

        Task<AnswerModel> CreateWithParentsAndChidrenAsync(AnswerModel model, IEnumerable<AnswerModel> parents, IEnumerable<AnswerModel> children);
    }
}
