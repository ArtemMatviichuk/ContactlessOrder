using ContactlessOrder.DAL.EF;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ContactlessOrder.DAL.Entities.ApplicationSettings;

namespace ContactlessOrder.BLL.Infrastructure.Configuration
{
    public class DbConfigurationProvider : ConfigurationProvider
    {
        private readonly Action<DbContextOptionsBuilder> _options;

        public DbConfigurationProvider(Action<DbContextOptionsBuilder> options)
        {
            _options = options;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ContactlessOrderContext>();

            _options(builder);

            Data.Clear();
        }
    }
}

