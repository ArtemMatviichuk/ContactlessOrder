using ContactlessOrder.DAL.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Orders
{
    public class OrderPositionModificationConfiguration : IEntityTypeConfiguration<OrderPositionModification>
    {
        public void Configure(EntityTypeBuilder<OrderPositionModification> builder)
        {
            builder.ToTable("OrderPositionModification");
            builder.Property(e => e.ModificationName).HasMaxLength(250);

            builder.HasOne(e => e.Modification).WithMany().HasForeignKey(e => e.ModificationId);
            builder.HasOne(e => e.OrderPosition).WithMany(e => e.Modifications).HasForeignKey(e => e.OrderPositionId);
        }
    }
}
