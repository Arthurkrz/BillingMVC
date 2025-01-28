using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.Validators;
using BillingMVC.Service;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BillingMVC.IOC
{
    public static class DependencyInjection
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IBillService, BillService>();
        }

        public static void InjectRepositories(this IServiceCollection services)
        {

        }

        public static void InjectValidator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(BillValidator).Assembly);
        }
    }
}
