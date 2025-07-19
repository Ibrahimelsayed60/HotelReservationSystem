using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Offer.Offers.Dtos;
using Offer.Offers.Features.GetAllOffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Features.GetApplicableOffersForDateRange
{

    public record GetApplicableOffersForDateRangeResponse(IEnumerable<OfferDto> Offers);

    public class GetApplicableOffersForDateRangeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/offers/applicable", async (DateTime checkInDate, DateTime checkOutDate, ISender sender) =>
            {
                var result = await sender.Send(new GetApplicableOffersForDateRangeQuery(checkInDate, checkOutDate));

                var response = new GetApplicableOffersForDateRangeResult(result.Offers);

                return Results.Ok(response);
            })
                .WithName("GetApplicableOffersForDateRange")
                .Produces<GetApplicableOffersForDateRangeResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Applicable Offers For Date Range")
                .WithDescription("Get Applicable Offers For Date Range")
                ;
        }
    }
}
