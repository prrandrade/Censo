namespace Censo.Infra.Data.Configurations
{
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RegionConfiguration : IEntityTypeConfiguration<RegionModel>
    {
        public void Configure(EntityTypeBuilder<RegionModel> builder)
        {
            builder.HasData(
                new RegionModel { Id = 1, Value = "Sudeste" },
                new RegionModel { Id = 2, Value = "Sul" },
                new RegionModel { Id = 3, Value = "Centro-Oeste" },
                new RegionModel { Id = 4, Value = "Nordeste" },
                new RegionModel { Id = 5, Value = "Norte" }
            );
        }
    }
}
