using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.Property(e => e.Name).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Address).HasMaxLength(500);
            builder.Property(e => e.LogoPath).HasMaxLength(500);

            builder.Property(e => e.RegisteredDate)
                .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc));

            builder.Property(e => e.ModifiedDate)
                .HasConversion(d => d, d => DateTime.SpecifyKind(d.Value, DateTimeKind.Utc));

            builder.HasOne(e => e.User)
                .WithOne(e => e.Company)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
