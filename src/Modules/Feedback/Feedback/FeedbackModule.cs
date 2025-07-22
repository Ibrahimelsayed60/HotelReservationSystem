using Feedback.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.Data.Interceptors;
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
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();



            services.AddDbContext<FeedbackDbContext>((sp, options) =>
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

        public static IApplicationBuilder UseFeedbackModule(this IApplicationBuilder app)
        {
            app.UseMigration<FeedbackDbContext>();

            return app;
        }

    }
}
