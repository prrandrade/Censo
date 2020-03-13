namespace Censo.Infra.Data.Repositories
{
    using Domain.Interfaces.Data;
    using Domain.Model;

    public class RegionRepository : RepositoryBase<RegionModel>, IRegionRepository
    {
        public RegionRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
