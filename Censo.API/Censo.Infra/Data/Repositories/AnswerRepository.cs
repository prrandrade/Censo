namespace Censo.Infra.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Domain;
    using Domain.Interfaces.Data;
    using Domain.Model;
    using LinqKit;
    using Microsoft.EntityFrameworkCore;

    public class AnswerRepository : RepositoryBase<AnswerModel>, IAnswerRepository
    {
        public AnswerRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

        public override async Task<AnswerModel> GetAsync(int id)
        {
            await Task.CompletedTask;
            var result = DatabaseContext
                .Answer
                .Include(x => x.Parents)
                .ThenInclude(x => x.Parent)
                .Include(x => x.Children)
                .ThenInclude(x => x.Child).FirstOrDefault(x => x.Id == id);

            return result;
        }

        public async Task<AnswerModel> CreateWithParentsAndChidrenAsync(AnswerModel model, IEnumerable<AnswerModel> parents, IEnumerable<AnswerModel> children)
        {
            await using var transaction = DatabaseContext.Database.BeginTransaction();
            try
            {
                // filling the remaining options, if this person is already recorded as a parent or child
                var oldModel = await GetByNameAsync(model.FirstName, model.LastName);
                if (oldModel == null)
                {
                    await CreateAsync(model);
                }
                else
                {
                    DatabaseContext.Entry(oldModel).CurrentValues.SetValues(model);
                    await DatabaseContext.SaveChangesAsync();
                }

                // saving parents
                if (parents != null)
                    foreach (var parent in parents)
                    {
                        var updatedParent = await GetByNameAsync(parent.FirstName, parent.LastName) ?? await CreateAsync(parent);
                        DatabaseContext.AnswerParentChild.Add(new AnswerParentChildModel { Parent = updatedParent, Child = model });
                        await DatabaseContext.SaveChangesAsync();
                    }

                // saving children
                if (children != null)
                    foreach (var child in children)
                    {
                        var updatedChild = await GetByNameAsync(child.FirstName, child.LastName) ?? await CreateAsync(child);
                        DatabaseContext.AnswerParentChild.Add(new AnswerParentChildModel { Parent = model, Child = updatedChild });
                        await DatabaseContext.SaveChangesAsync();
                    }

                await transaction.CommitAsync();
                return model;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new ApplicationException("Error while saving this census anwser", ex);
            }
        }

        public async Task<IEnumerable<IEnumerable<DashboardModel>>> DashboardCount()
        {
            var regions = await DatabaseContext.Database.GetDbConnection().QueryAsync<DashboardModel>("select count(census_region.Value) as Count, census_Region.Value as Value from census_answers inner join Census_Region on census_answers.RegionId = Census_Region.Id group by (census_region.Value)");
            var genders = await DatabaseContext.Database.GetDbConnection().QueryAsync<DashboardModel>("select count(census_gender.Value) as Count, census_gender.Value as Value from census_answers inner join Census_gender on census_answers.genderId = Census_gender.Id group by (census_gender.Value)");
            var schoolings = await DatabaseContext.Database.GetDbConnection().QueryAsync<DashboardModel>("select count(census_schooling.Value) as Count, census_schooling.Value as Value from census_answers inner join Census_schooling on census_answers.schoolingId = Census_schooling.Id group by (census_schooling.Value)");
            var ethnicities = await DatabaseContext.Database.GetDbConnection().QueryAsync<DashboardModel>("select count(census_ethnicity.Value) as Count, census_ethnicity.Value as Value from census_answers inner join Census_ethnicity on census_answers.ethnicityId = Census_ethnicity.Id group by (census_ethnicity.Value)");

            return new[] { regions, genders, schoolings, ethnicities };
        }

        #region Queries for filtering

        public async Task<(int searchResult, int total)> ApplyFilterAsync(string name, NameComparisonEnum? nameComparison, int? region, int? gender, int? ethnicity, int? schooling)
        {
            var total = await DatabaseContext.Answer.CountAsync();

            var predicate = PredicateBuilder.New<AnswerModel>(true);

            if (!string.IsNullOrWhiteSpace(name))
                switch (nameComparison)
                {
                    case NameComparisonEnum.StartWith:
                        predicate = predicate.And(x => x.FirstName.ToLower().StartsWith(name.ToLower()) || x.LastName.ToLower().StartsWith(name.ToLower()));
                        break;
                    case NameComparisonEnum.EndsWith:
                        predicate = predicate.And(x => x.FirstName.ToLower().EndsWith(name.ToLower()) || x.LastName.ToLower().EndsWith(name.ToLower()));
                        break;
                    case NameComparisonEnum.Equals:
                        predicate = predicate.And(x => x.FirstName.ToLower().Equals(name.ToLower()) || x.LastName.ToLower().Equals(name.ToLower()));
                        break;
                    default:
                        predicate = predicate.And(x => x.FirstName.ToLower().Contains(name.ToLower()) || x.LastName.ToLower().Contains(name.ToLower()));
                        break;
                }
            if (region.HasValue)
                predicate = predicate.And(x => x.RegionId == region);
            if (gender.HasValue)
                predicate = predicate.And(x => x.GenderId == gender);
            if (ethnicity.HasValue)
                predicate = predicate.And(x => x.EthnicityId == ethnicity);
            if (schooling.HasValue)
                predicate = predicate.And(x => x.SchoolingId == schooling);

            var searchResult = await DatabaseContext.Answer.CountAsync(predicate);

            return (searchResult, total);
        }

        public async Task<List<List<AnswerModel>>> ApplyGenealogyFilter(int id, int parentMaxLevel = 0)
        {
            // the first node in a genealogic tree is the passed target
            var tree = new List<List<AnswerModel>> { new List<AnswerModel>() };
            tree[0].Add(await GetAsync(id));

            var childIds = new[] { id };
            // now we will retrieve the parent levels
            for (var i = 0; i < parentMaxLevel; i++)
            {
                var ids = await ApplyGenealogyGetParentsAsync(childIds);
                if (ids.Any())
                {
                    tree.Add(DatabaseContext.Answer.Where(x => ids.Contains(x.Id)).ToList());
                }
                childIds = ids;
            }

            return tree;
        }



        #endregion

        #region Auxiliary private methods

        private async Task<AnswerModel> GetByNameAsync(string firstName, string lastName)
        {
            await Task.CompletedTask;
            return DatabaseContext.Answer
                .FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }

        private async Task<int[]> ApplyGenealogyGetParentsAsync(params int[] ids)
        {
            return await DatabaseContext.AnswerParentChild.Where(x => ids.ToList().Contains(x.ChildId)).Select(x => x.ParentId).ToArrayAsync();
        }

        #endregion
    }
}
