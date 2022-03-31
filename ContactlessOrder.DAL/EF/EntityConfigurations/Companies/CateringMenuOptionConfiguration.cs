using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class CateringMenuOptionConfiguration : IEntityTypeConfiguration<CateringMenuOption>
    {
        public void Configure(EntityTypeBuilder<CateringMenuOption> builder)
        {
            builder.ToTable("CateringMenuOptions");

            builder.HasOne(e => e.MenuOption).WithMany().HasForeignKey(e => e.MenuOptionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Catering).WithMany(e => e.MenuOptions).HasForeignKey(e => e.CateringId);
        }
    }
}
