using ContactlessOrder.DAL.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasOne(e => e.Status).WithMany().HasForeignKey(e => e.StatusId);
            builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.PaymentMethod).WithMany().HasForeignKey(e => e.PaymentMethodId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
