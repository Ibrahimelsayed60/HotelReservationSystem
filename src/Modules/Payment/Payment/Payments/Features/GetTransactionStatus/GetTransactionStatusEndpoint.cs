using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Payment.Payments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Features.GetTransactionStatus
{
    public record GetTransactionStatusResponse(TransactionStatusDto TransactionStatus);
    public class GetTransactionStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/payment/reservations/{reservationId:guid}/status", async (Guid reservationId, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetTransactionStatusQuery(reservationId), cancellationToken);

                var response = new GetTransactionStatusResponse(result.TransactionStatus);

                return Results.Ok(response);
            })
                .WithName("GetTransactionStatus")
                .Produces<GetTransactionStatusResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetTransactionStatus")
                .WithDescription("GetTransactionStatus")
                ;
        }
    }
}
