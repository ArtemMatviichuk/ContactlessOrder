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

            builder.HasOne(e => e.User)
                .WithOne(e => e.Company)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
