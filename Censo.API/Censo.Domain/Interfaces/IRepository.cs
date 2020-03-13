namespace Censo.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> CreateAsync(T t);

        Task UpdateAsync(T t);

        Task<bool> DeleteAsync(int id);
    }
}
