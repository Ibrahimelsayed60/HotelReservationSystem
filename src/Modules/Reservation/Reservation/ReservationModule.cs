using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservation.Data;
using Reservation.Reservations.Services;
using Shared.Data;
using Shared.Data.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation
{
    public static class ReservationModule
    {

        public static IServiceCollection AddReservationModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddScoped<IOfferCacheService, RedisOfferCacheService>();

            services.AddDbContext<ReservationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                
                options.UseNpgsql(connectionString);
            });

            return services;
        }

        public static IApplicationBuilder UseReservationModule(this IApplicationBuilder app)
        {
            app.UseMigration<ReservationDbContext>();

            return app;
        }

    }
}
