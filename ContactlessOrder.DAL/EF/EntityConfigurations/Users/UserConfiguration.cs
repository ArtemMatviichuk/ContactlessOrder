using ContactlessOrder.DAL.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ContactlessOrder.DAL.EF.EntityConfigurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(250);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(250);
            builder.Property(e => e.PasswordHash).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(250);
            builder.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(250);
            builder.Property(e => e.ProfilePhotoPath).HasMaxLength(500);

            builder.Property(e => e.RegistrationDate)
                .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc));

            builder.Property(e => e.ModifiedDate)
                .HasConversion(d => d, d => DateTime.SpecifyKind(d.Value, DateTimeKind.Utc));

            builder.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
