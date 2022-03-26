using ContactlessOrder.BLL.Infrastructure;
using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Entities.User;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ContactlessOrder.Api
{
    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ContactlessOrderContext>();
            context.Database.EnsureCreated();

            var user = new User()
            {
                FirstName = "ContactlessOrder",
                LastName = "Admin",
                PasswordHash = CryptoHelper.GetMd5Hash("123123"),
                Email = "contactless.order@gmail.com",
                PhoneNumber = "",
                EmailConfirmed = true,
                RegistrationDate = DateTime.Now,
            };

            context.Add(user);
            context.SaveChanges();
        }
    }
}
