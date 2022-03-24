using ContactlessOrder.DAL.EF;
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
        }
    }
}
