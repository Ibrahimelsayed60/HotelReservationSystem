using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            return services;
        }

        public static IApplicationBuilder UseOfferModule(this IApplicationBuilder app)
        {
            return app;
        }

    }
}
