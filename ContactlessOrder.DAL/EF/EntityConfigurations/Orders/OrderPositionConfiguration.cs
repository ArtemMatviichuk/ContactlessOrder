using ContactlessOrder.DAL.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Orders
{
    public class OrderPositionConfiguration : IEntityTypeConfiguration<OrderPosition>
    {
        public void Configure(EntityTypeBuilder<OrderPosition> builder)
        {
            builder.ToTable("OrderPositions");
            builder.Property(e => e.OptionName).HasMaxLength(250);

            builder.HasOne(e => e.Option).WithMany().HasForeignKey(e => e.OptionId);
            builder.HasOne(e => e.Order).WithMany(e => e.Positions).HasForeignKey(e => e.OrderId);
        }
    }
}
