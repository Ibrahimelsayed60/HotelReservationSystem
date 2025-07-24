using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Features.RetryPaymentSession
{

    public record RetryPaymentSessionResponse(string result);

    internal class RetryPaymentSessionEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/payment/reservations/{reservationId:guid}/retry", async (
            Guid reservationId,
            ISender sender,
            CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new RetryPaymentSessionCommand(reservationId), cancellationToken);

                var response = new RetryPaymentSessionResponse(result.result);

                return Results.Ok(response);
            })
                .WithName("RetryPaymentSession")
                .Produces<RetryPaymentSessionResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("RetryPaymentSession")
                .WithDescription("RetryPaymentSession")
                ;
        }
    }
}
