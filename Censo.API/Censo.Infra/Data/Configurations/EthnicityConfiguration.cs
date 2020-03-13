namespace Censo.Infra.Data.Configurations
{
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EthnicityConfiguration : IEntityTypeConfiguration<EthnicityModel>
    {
        public void Configure(EntityTypeBuilder<EthnicityModel> builder)
        {
            builder.HasData(
                new EthnicityModel { Id = 1, Value = "Branco" },
                new EthnicityModel { Id = 2, Value = "Pardo" },
                new EthnicityModel { Id = 3, Value = "Preto" },
                new EthnicityModel { Id = 4, Value = "Amarelo" },
                new EthnicityModel { Id = 5, Value = "Indígena" }
            );
        }
    }
}
