using System;
using Contracts.Entities;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Domain.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services
                .AddSingleton<IReceiptsService, ReceiptsService>()
                .AddSingleton<IFeeCalcService, FeeCalcService>()
                .AddSingleton<IPrintService, PrintService>()
                .AddSingleton<Fee>();
        }
    }
}
