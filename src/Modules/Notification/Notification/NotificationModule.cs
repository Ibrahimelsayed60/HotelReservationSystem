using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Shared.Data.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Data;
using Notification.Notifications.Service;

namespace Notification
{
    public static class NotificationModule
    {

        public static IServiceCollection AddNotificationModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddScoped<ISmtpService, SmtpService>();

            //services.AddScoped<IOfferCacheService, RedisOfferCacheService>();

            services.AddDbContext<NotificationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                options.UseNpgsql(connectionString);
            });

            return services;
        }


        public static IApplicationBuilder UseNotificationModule(this IApplicationBuilder app)
        {
            app.UseMigration<NotificationDbContext>();

            return app;
        }

    }
}
