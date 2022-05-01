using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class PaymentDataConfiguration : IEntityTypeConfiguration<PaymentData>
    {
        public void Configure(EntityTypeBuilder<PaymentData> builder)
        {
            builder.ToTable("PaymentData");

            builder.Property(e => e.Name).HasMaxLength(250);
            builder.Property(e => e.Bank).HasMaxLength(250);
            builder.Property(e => e.Mfo).HasMaxLength(250);
            builder.Property(e => e.LegalEntityCode).HasMaxLength(250);
            builder.Property(e => e.CurrentAccount).HasMaxLength(250);
            builder.Property(e => e.Mfo).HasMaxLength(250);

            builder.HasOne(e => e.Company).WithOne(e => e.PaymentData).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
