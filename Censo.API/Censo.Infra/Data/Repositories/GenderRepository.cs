namespace Censo.Infra.Data.Repositories
{
    using Domain.Interfaces.Data;
    using Domain.Model;

    public class GenderRepository : RepositoryBase<GenderModel>, IGenderRepository
    {
        public GenderRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
