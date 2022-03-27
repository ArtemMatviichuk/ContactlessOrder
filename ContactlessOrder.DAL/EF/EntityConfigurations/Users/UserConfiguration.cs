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

            builder.Property(e => e.ExpireDate)
                .HasConversion(d => d, d => DateTime.SpecifyKind(d.Value, DateTimeKind.Utc));

            builder.HasData(new User()
            {
                Id = -1,
                FirstName = "ContactlessOrder",
                LastName = "Admin",
                PasswordHash = GetMd5Hash("123123"),
                Email = "contactless.order@gmail.com",
                PhoneNumber = "",
                EmailConfirmed = true,
                RegistrationDate = new DateTime(2022, 2, 24),
            });
        }

        public static string GetMd5Hash(string input)
        {
            var md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
