using BillingMVC.Core.Contracts.Mapping;
using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Data.Repositories;
using BillingMVC.IOC;
using BillingMVC.Service;
using BillingMVC.Web.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace BillingMVC.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var redisConnectionString = Configuration["Redis:ConnectoinString"];
            services.AddSingleton<IConnectionMultiplexer>(c => ConnectionMultiplexer.Connect(redisConnectionString));
            string test = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            var stringDb = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Context>(options => options.UseSqlServer(stringDb));
            services.AddMemoryCache();
            services.AddControllersWithViews();
            services.InjectValidator();
            services.InjectExternalServices();
            services.InjectServices();
            services.InjectRepositories();
            services.AddSingleton<IMap, MappingProfile>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBillService, BillService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
