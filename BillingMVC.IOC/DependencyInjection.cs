using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.Contracts.ExternalServices;
using BillingMVC.Core.Validators;
using BillingMVC.Data.Repositories;
using BillingMVC.Service;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using BillingMVC.ExternalServices;
using BillingMVC.Service.Utilities;

namespace BillingMVC.IOC
{
    public static class DependencyInjection
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IExchangeService, ExchangeService>();
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBillRepository, BillRepository>();
        }

        public static void InjectValidator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(BillValidator).Assembly);
        }

        public static void InjectExternalServices(this IServiceCollection services)
        {
            services.AddScoped<IExchangeHandler, ExchangeHandler>();
        }
    }
}