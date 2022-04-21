using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class ModificationConfiguration : IEntityTypeConfiguration<Modification>
    {
        public void Configure(EntityTypeBuilder<Modification> builder)
        {
            builder.ToTable("Modifications");
            builder.Property(e => e.Name).HasMaxLength(250);

            builder.HasOne(e => e.Company).WithMany(e => e.Modifications).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
