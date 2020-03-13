namespace Censo.Infra.Data
{
    using Domain.Interfaces;
    using Domain.Interfaces.Data;
    using Domain.Model;

    public class RegionRepository : RepositoryBase<RegionModel>, IRegionRepository
    {
        public RegionRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
