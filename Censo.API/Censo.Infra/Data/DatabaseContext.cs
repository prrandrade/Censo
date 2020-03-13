namespace Censo.Infra.Data
{
    using Configurations;
    using Domain.Interfaces;
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext
    {
        private readonly IMyEnvironment _myEnvironment;

        public DbSet<RegionModel> Regions { get; set; }

        public DbSet<GenderModel> Genders { get; set; }

        public DbSet<EthnicityModel> Ethnicities { get; set; }

        public DbSet<SchoolingModel> Schoolings { get; set; }

        public DatabaseContext(IMyEnvironment myEnvironment)
        {
            _myEnvironment = myEnvironment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var datasource = _myEnvironment.GetVariable("SQL_SERVER_HOST");
            var database = _myEnvironment.GetVariable("SQL_SERVER_DATABASE");
            var user = _myEnvironment.GetVariable("SQL_SERVER_USER");
            var password = _myEnvironment.GetVariable("SQL_SERVER_PASSWORD");

            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer($"Data Source={datasource};Initial Catalog={database};Persist Security Info=True;User ID={user};Password={password}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegionConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolingConfiguration());
            modelBuilder.ApplyConfiguration(new EthnicityConfiguration());
        }
    }
}
