namespace Censo.Infra.Data.Configurations
{
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AnswerParentChildConfiguration : IEntityTypeConfiguration<AnswerParentChildModel>
    {
        public void Configure(EntityTypeBuilder<AnswerParentChildModel> builder)
        {
            builder.ToTable("Census_AnswersParentChild");
            builder.HasKey(c => new {c.ParentId, c.ChildId});
            builder.HasOne(c => c.Parent).WithMany(c => c.Children).HasForeignKey(sc => sc.ParentId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.Child).WithMany(c => c.Parents).HasForeignKey(c => c.ChildId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
