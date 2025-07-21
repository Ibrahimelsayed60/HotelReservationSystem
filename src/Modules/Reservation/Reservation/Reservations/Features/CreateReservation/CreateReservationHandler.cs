using MassTransit;
using Reservation.Data;
using Reservation.Reservations.Dtos;
using Reservation.Reservations.Models;
using Reservation.Reservations.Services;
using Shared.Contracts.CQRS;
using Shared.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Reservations.Features.CreateReservation
{

    public record CreateReservationCommand(ReservationDto reservation):ICommand<CreateReservationResult>;

    public record CreateReservationResult(Guid Id);

    public class CreateReservationHandler(ReservationDbContext dbContext, IOfferCacheService offerCache, IBus bus) : ICommandHandler<CreateReservationCommand, CreateReservationResult>
    {
        public async Task<CreateReservationResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = CreateNewReservarion(request.reservation);

            var offers = await offerCache.GetAllOffersAsync();

            var bestOffer = offers
                .Where(o =>
                    o.StartDate <= request.reservation.CheckInDate &&
                    o.EndDate >= request.reservation.CheckOutDate &&
                    (!o.RoomIds.Any() || o.RoomIds.Contains(request.reservation.RoomId))
                )
                .OrderByDescending(o => o.DiscountPercentage)
                .FirstOrDefault();

            if (bestOffer != null)
            {
                reservation.ApplyOffer(bestOffer.OfferId, (double)bestOffer.DiscountPercentage);
            }


            dbContext.Add(reservation);

            await dbContext.SaveChangesAsync();

            await bus.Send(new RoomReservedIntegrationEvent
            {
                ReservationId = reservation.Id,
                RoomId = reservation.RoomId,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
            }, cancellationToken);

            await bus.Send(new ReservationCreatedIntegrationEvent
            {
                ReservationId = reservation.Id,
                UserId = request.reservation.UserId,
                RoomId = request.reservation.RoomId,
                CheckInDate = request.reservation.CheckInDate,
                CheckOutDate = request.reservation.CheckOutDate,
                BasePrice = (decimal)reservation.TotalPriceIfOfferExistOrNot,
                CreatedAt = DateTime.UtcNow
            }, cancellationToken);

            return new CreateReservationResult(reservation.Id);
        }

        private Reservation.Reservations.Models.Reservation CreateNewReservarion(ReservationDto reservation) 
        {
            var reservationCreated = Reservation.Reservations.Models.Reservation.Create(
                reservation.UserId,
                reservation.RoomId,
                reservation.CheckInDate,
                reservation.CheckOutDate,
                reservation.RoomName,
                reservation.RoomType,
                reservation.TotalPrice
                );
            return reservationCreated;
        }


    }
}
