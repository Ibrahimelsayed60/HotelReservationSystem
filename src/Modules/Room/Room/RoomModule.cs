using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Room.Data;
using Shared.Data.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Room
{
    public static class RoomModule
    {

        public static IServiceCollection AddRoomModule(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<RoomDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                //options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure(
                //                maxRetryCount: 5,
                //                maxRetryDelay: TimeSpan.FromSeconds(10),
                //                errorNumbersToAdd: null));
                options.UseNpgsql(connectionString);
            });

            return services;
        }

        public static IApplicationBuilder UseRoomModule(this IApplicationBuilder app)
        {
            return app;
        }

    }
}
