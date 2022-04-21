using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class MenuItemModificationConfiguration : IEntityTypeConfiguration<MenuItemModification>
    {
        public void Configure(EntityTypeBuilder<MenuItemModification> builder)
        {
            builder.ToTable("MenuItemModifications");

            builder.HasOne(e => e.MenuItem).WithMany(e => e.MenuItemModifications).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Modification).WithMany().HasForeignKey(e => e.ModificationId);
        }
    }
}
