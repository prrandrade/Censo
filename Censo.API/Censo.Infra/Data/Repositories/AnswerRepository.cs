namespace Censo.Infra.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Interfaces.Data;
    using Domain.Model;
    using Microsoft.Win32.SafeHandles;

    public class AnswerRepository : RepositoryBase<AnswerModel>, IAnswerRepository
    {
        public AnswerRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }

        public async Task<AnswerModel> CreateWithParentsAndChidrenAsync(AnswerModel model, IEnumerable<AnswerModel> parents, IEnumerable<AnswerModel> children)
        {
            using (var transaction = DatabaseContext.Database.BeginTransaction())
            {
                try
                {
                    await CreateAsync(model);

                    // saving parents
                    if (parents != null)
                        foreach (var parent in parents)
                        {
                            var updatedParent = await GetByNameAsync(parent.FirstName, parent.LastName) ?? await CreateAsync(parent);
                            DatabaseContext.AnswerParentChild.Add(new AnswerParentChildModel {Parent = updatedParent, Child = model});
                            await DatabaseContext.SaveChangesAsync();
                        }

                    // saving children
                    if (children != null)
                        foreach (var child in children)
                        {
                            var updatedChild = await GetByNameAsync(child.FirstName, child.LastName) ?? await CreateAsync(child);
                            DatabaseContext.AnswerParentChild.Add(new AnswerParentChildModel {Parent = model, Child = updatedChild});
                            await DatabaseContext.SaveChangesAsync();
                        }

                    transaction.Commit();
                   return model;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ApplicationException("Error while saving this census anwser", ex);
                }
            }
        }


        public async Task<AnswerModel> GetByNameAsync(string firstName, string lastName)
        {
            await Task.CompletedTask;
            return DatabaseContext.Answer
                .FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }
    }
}
