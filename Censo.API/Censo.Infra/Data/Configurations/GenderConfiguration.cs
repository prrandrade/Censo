namespace Censo.Infra.Data.Configurations
{
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GenderConfiguration : IEntityTypeConfiguration<GenderModel>
    {
        public void Configure(EntityTypeBuilder<GenderModel> builder)
        {
            builder.ToTable("Census_Gender");
            builder.HasKey(c =>c.Id);
            builder.Property(c => c.Value).IsRequired();

            builder.HasData(
                new GenderModel { Id = 1, Value = "Masculino" },
                new GenderModel { Id = 2, Value = "Feminino" }
            );
        }
    }
}
