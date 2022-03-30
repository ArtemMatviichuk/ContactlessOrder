using ContactlessOrder.DAL.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Companies
{
    public class MenuItemPictureConfiguration : IEntityTypeConfiguration<MenuItemPicture>
    {
        public void Configure(EntityTypeBuilder<MenuItemPicture> builder)
        {
            builder.ToTable("MenuPictures");

            builder.Property(e => e.CreatedDate)
                .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc));

            builder.HasOne(e => e.MenuItem).WithMany(e => e.Pictures).HasForeignKey(e => e.MenuItemId);
        }
    }
}
