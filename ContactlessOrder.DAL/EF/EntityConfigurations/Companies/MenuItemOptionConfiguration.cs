using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class MenuItemOptionConfiguration : IEntityTypeConfiguration<MenuItemOption>
    {
        public void Configure(EntityTypeBuilder<MenuItemOption> builder)
        {
            builder.ToTable("MenuOptions");

            builder.Property(e => e.Name).HasMaxLength(250);

            builder.HasOne(e => e.MenuItem).WithMany(e => e.Options).HasForeignKey(e => e.MenuItemId);
        }
    }
}
