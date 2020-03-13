namespace Censo.Infra.Data.Configurations
{
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SchoolingConfiguration : IEntityTypeConfiguration<SchoolingModel>
    {
        public void Configure(EntityTypeBuilder<SchoolingModel> builder)
        {
            builder.HasData(
                new SchoolingModel { Id = 1, Value = "Fundamental I Incompleto" },
                new SchoolingModel { Id = 2, Value = "Fundamental I Completo" },
                new SchoolingModel { Id = 3, Value = "Fundamental II Incompleto" },
                new SchoolingModel { Id = 4, Value = "Fundamental II Completo" },
                new SchoolingModel { Id = 5, Value = "Médio Imcompleto" },
                new SchoolingModel { Id = 6, Value = "Médio Completo" },
                new SchoolingModel { Id = 7, Value = "Superior Incompleto" },
                new SchoolingModel { Id = 8, Value = "Superior Completo" }
            );
        }
    }
}
