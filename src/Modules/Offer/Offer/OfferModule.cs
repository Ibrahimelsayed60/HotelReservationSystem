using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Offer.Data;
using Shared.Data;
using Shared.Data.Interceptors;
using Shared.Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer
{
    public static class OfferModule
    {

        public static IServiceCollection AddOfferModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            

            services.AddDbContext<OfferDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                //options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure(
                //                maxRetryCount: 5,
                //                maxRetryDelay: TimeSpan.FromSeconds(10),
                //                errorNumbersToAdd: null));
                options.UseNpgsql(connectionString);
            });

            //services.AddScoped<IDataSeeder, RoomContextSeed>();

            return services;
        }

        public static IApplicationBuilder UseOfferModule(this IApplicationBuilder app)
        {

            app.UseMigration<OfferDbContext>();

            return app;
        }

    }
}
