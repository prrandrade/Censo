namespace Censo.Infra.Data.Repositories
{
    using Domain.Interfaces.Data;
    using Domain.Model;

    public class EthnicityRepository : RepositoryBase<EthnicityModel>, IEthnicityRepository
    {
        public EthnicityRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
