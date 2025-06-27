using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback
{
    public static class FeedbackModule
    {

        public static IServiceCollection AddFeedbackModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IApplicationBuilder UseFeedbackModule(this IApplicationBuilder app)
        {
            return app;
        }

    }
}
