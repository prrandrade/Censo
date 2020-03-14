namespace Censo.Infra.Data.Configurations
{
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AnswerConfiguration : IEntityTypeConfiguration<AnswerModel>
    {
        public void Configure(EntityTypeBuilder<AnswerModel> builder)
        {
            builder.ToTable("Census_Answers");
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => new {c.FirstName, c.LastName}).IsUnique();

            builder.HasOne(c => c.Gender).WithMany().HasForeignKey(c => c.GenderId);
            builder.HasOne(c => c.Region).WithMany().HasForeignKey(c => c.RegionId);
            builder.HasOne(c => c.Ethnicity).WithMany().HasForeignKey(c => c.EthnicityId);
            builder.HasOne(c => c.Schooling).WithMany().HasForeignKey(c => c.SchoolingId);

            builder.HasIndex(c => new {c.FirstName, c.LastName, c.GenderId, c.RegionId, c.EthnicityId, c.SchoolingId});
        }
    }
}
