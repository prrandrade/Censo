namespace Censo.Infra.Data
{
    using Configurations;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext
    {
        public DbSet<AnswerModel> Answer { get; set; }
        public DbSet<AnswerParentChildModel> AnswerParentChild { get; set; }

        public DbSet<RegionModel> Regions { get; set; }
        public DbSet<GenderModel> Genders { get; set; }
        public DbSet<EthnicityModel> Ethnicities { get; set; }
        public DbSet<SchoolingModel> Schoolings { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnswerParentChildConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new RegionConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolingConfiguration());
            modelBuilder.ApplyConfiguration(new EthnicityConfiguration());
        }
    }
}
