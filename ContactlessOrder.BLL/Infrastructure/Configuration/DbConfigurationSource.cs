using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ContactlessOrder.BLL.Infrastructure.Configuration
{
    public class DbConfigurationSource : IConfigurationSource
    {
        private readonly Action<DbContextOptionsBuilder> _options;

        public DbConfigurationSource(Action<DbContextOptionsBuilder> options)
        {
            _options = options;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DbConfigurationProvider(_options);
        }
    }
}
