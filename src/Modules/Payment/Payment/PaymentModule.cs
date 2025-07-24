using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Payments.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.Paymob.CashIn;

namespace Payment
{
    public static class PaymentModule
    {

        public static IServiceCollection AddPaymentModule(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddPaymobCashIn(options =>
            {
                options.ApiKey = configuration["Paymob:ApiKey"];
                //options.IntegrationId = int.Parse(configuration["Paymob:IntegrationId"]);
            });

            services.AddScoped<IPaymobHmacValidator, PaymobHmacValidator>();

            return services;
        }

        public static IApplicationBuilder UsePaymentModule(this IApplicationBuilder app)
        {
            return app;
        }

    }
}
