using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Features.HandlePaymobWebhook
{
    public class PaymobWebhookEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/payment/paymob/webhook", async (HttpRequest request, ISender sender, ILogger<PaymobWebhookEndpoint> logger, CancellationToken cancellationToken) =>
            {
                using var reader = new StreamReader(request.Body);
                var payload = await reader.ReadToEndAsync(cancellationToken);

                // Read HMAC from Paymob header
                var hmac = request.Headers["hmac"].FirstOrDefault();
                if (string.IsNullOrWhiteSpace(hmac))
                {
                    logger.LogWarning("Missing HMAC header from Paymob.");
                    return Results.BadRequest("Missing HMAC.");
                }

                var command = new HandlePaymobWebhookCommand(payload, hmac);
                var result = await sender.Send(command, cancellationToken);

                return result.result=="Success" ? Results.Ok() : Results.BadRequest(result.result);
            })
               .WithName("PaymobWebhook")
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("PaymobWebhook")
                .WithDescription("PaymobWebhook")
                ;
        }
    }
}
