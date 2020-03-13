namespace Censo.Infra.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Interfaces;

    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IModel
    {
        private readonly DatabaseContext _databaseContext;

        protected RepositoryBase(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<T> CreateAsync(T t)
        {
            var newInfo = _databaseContext.Set<T>().Add(t);
            await _databaseContext.SaveChangesAsync();
            return newInfo.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var info = await GetAsync(id);
            if (info == null) return false;
            _databaseContext.Set<T>().Remove(info);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<T> GetAsync(int id)
        {
            await Task.CompletedTask;
            return _databaseContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            await Task.CompletedTask;
            return _databaseContext.Set<T>().OrderBy(x => x.Id);
        }

        public async Task UpdateAsync(T t)
        {
            var old = _databaseContext.Set<T>().Find(t.Id);
            _databaseContext.Entry(old).CurrentValues.SetValues(t);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
