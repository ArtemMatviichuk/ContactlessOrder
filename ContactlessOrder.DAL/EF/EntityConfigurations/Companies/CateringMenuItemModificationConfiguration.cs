using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class CateringModificationConfiguration : IEntityTypeConfiguration<CateringModification>
    {
        public void Configure(EntityTypeBuilder<CateringModification> builder)
        {
            builder.ToTable("CateringModifications");

            builder.HasOne(e => e.Catering).WithMany(e => e.CateringModifications).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Modification).WithMany().HasForeignKey(e => e.ModificationId);
        }
    }
}
