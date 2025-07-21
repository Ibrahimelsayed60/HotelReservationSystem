using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Reporting.Data;
using Shared.Data.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Data;

namespace Reporting
{
    public static class ReportingModule
    {

        public static IServiceCollection AddReportingModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<ReportingDbContext>((sp, options) =>
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

        public static IApplicationBuilder UseReportingModule(this IApplicationBuilder app)
        {
            app.UseMigration<ReportingDbContext>();

            return app;
        }

    }
}
