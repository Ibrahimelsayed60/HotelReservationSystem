using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room
{
    public static class RoomModule
    {

        public static IServiceCollection AddRoomModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IApplicationBuilder UseRoomModule(this IApplicationBuilder app)
        {
            return app;
        }

    }
}
