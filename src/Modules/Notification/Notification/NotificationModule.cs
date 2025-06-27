using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    public static class NotificationModule
    {

        public static IServiceCollection AddNotificationModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }


        public static IApplicationBuilder UseNotificationModule(this IApplicationBuilder app)
        {
            return app;
        }

    }
}
