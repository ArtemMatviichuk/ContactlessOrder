using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class CateringMenuModificationConfiguration : IEntityTypeConfiguration<CateringMenuModification>
    {
        public void Configure(EntityTypeBuilder<CateringMenuModification> builder)
        {
            builder.ToTable("CateringMenuModifications");

            builder.HasOne(e => e.MenuModification).WithMany().HasForeignKey(e => e.MenuModificationId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Catering).WithMany(e => e.MenuModifications).HasForeignKey(e => e.CateringId);
        }
    }
}
