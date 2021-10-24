using System;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace CongestionApp
{
    public class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services
                .AddPersistence()
                .AddDomain();

            services.AddSingleton<CongestionApp>();

            return services.BuildServiceProvider();
        }
    }
}
