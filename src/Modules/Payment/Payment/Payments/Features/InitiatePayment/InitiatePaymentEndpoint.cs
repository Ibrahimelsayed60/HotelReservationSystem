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

namespace Payment.Payments.Features.InitiatePayment
{
    public record InitiatePaymentRequest(Guid ReservationId,
    Guid UserId,
    decimal Amount);

    public record InitiatePaymentResponse(string frame);

    internal class InitiatePaymentEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/payment/initiate", async (
            InitiatePaymentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new InitiatePaymentCommand(request.ReservationId, request.UserId, request.Amount));

                var response = new InitiatePaymentResponse(result.frame);

                return Results.Ok(response);

            })
                .WithName("InitiatePayment")
                .Produces<InitiatePaymentResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("InitiatePayment")
                .WithDescription("InitiatePayment")
                ;
        }
    }
}
