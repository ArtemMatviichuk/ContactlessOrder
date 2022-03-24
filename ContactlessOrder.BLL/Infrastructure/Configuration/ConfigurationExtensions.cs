using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ContactlessOrder.BLL.Infrastructure.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddDbConfigurationProvider(this IConfigurationBuilder configuration,
            Action<DbContextOptionsBuilder> setup)
        {
            configuration.Add(new DbConfigurationSource(setup));
            return configuration;
        }
    }
}
