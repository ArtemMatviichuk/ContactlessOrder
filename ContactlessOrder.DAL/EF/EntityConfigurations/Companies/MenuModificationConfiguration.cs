using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class MenuModificationConfiguration : IEntityTypeConfiguration<MenuModification>
    {
        public void Configure(EntityTypeBuilder<MenuModification> builder)
        {
            builder.ToTable("MenuModifications");

            builder.Property(e => e.Name).HasMaxLength(250);

            builder.HasOne(e => e.MenuItem).WithMany(e => e.Modifications).HasForeignKey(e => e.MenuItemId);
        }
    }
}
