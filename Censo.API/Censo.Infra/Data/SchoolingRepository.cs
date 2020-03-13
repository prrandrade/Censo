namespace Censo.Infra.Data
{
    using Domain.Interfaces.Data;
    using Domain.Model;

    public class SchoolingRepository : RepositoryBase<SchoolingModel>, ISchoolingRepository
    {
        public SchoolingRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
