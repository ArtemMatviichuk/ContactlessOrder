using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class CateringConfiguration : IEntityTypeConfiguration<Catering>
    {
        public void Configure(EntityTypeBuilder<Catering> builder)
        {
            builder.ToTable("Caterings");

            builder.Property(e => e.Name).IsRequired().HasMaxLength(250);

            builder.Property(e => e.RegisteredDate)
                .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc));

            builder.Property(e => e.ModifiedDate)
                .HasConversion(d => d, d => DateTime.SpecifyKind(d.Value, DateTimeKind.Utc));

            builder.HasOne(e => e.Company)
                .WithMany(e => e.Caterings)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Coordinates).WithOne(e => e.Catering);
            builder.HasOne(e => e.User).WithOne(e => e.Catering).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
