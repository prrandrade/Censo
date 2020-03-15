namespace Censo.Infra.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Interfaces;

    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IModel
    {
        protected readonly DatabaseContext DatabaseContext;

        protected RepositoryBase(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public virtual async Task<T> CreateAsync(T t)
        {
            var newInfo = DatabaseContext.Set<T>().Add(t);
            await DatabaseContext.SaveChangesAsync();
            return newInfo.Entity;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            await Task.CompletedTask;
            return DatabaseContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            await Task.CompletedTask;
            return DatabaseContext.Set<T>().OrderBy(x => x.Id);
        }

        public virtual async Task UpdateAsync(T t)
        {
            var old = DatabaseContext.Set<T>().Find(t.Id);
            DatabaseContext.Entry(old).CurrentValues.SetValues(t);
            await DatabaseContext.SaveChangesAsync();
        }
    }
}
