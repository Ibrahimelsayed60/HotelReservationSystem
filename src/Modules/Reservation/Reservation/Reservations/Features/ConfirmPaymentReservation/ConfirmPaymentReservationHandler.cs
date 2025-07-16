using Microsoft.EntityFrameworkCore;
using Reservation.Data;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static MassTransit.ValidationResultExtensions;

namespace Reservation.Reservations.Features.ConfirmPaymentReservation
{
    public record ConfirmReservationPaymentCommand(Guid ReservationId, decimal Amount, string PaymentMethod):ICommand<ConfirmPaymentReservationResult>;

    public record ConfirmPaymentReservationResult(bool IsSuccess);
    public class ConfirmPaymentReservationHandler(ReservationDbContext dbContext) : ICommandHandler<ConfirmReservationPaymentCommand, ConfirmPaymentReservationResult>
    {
        public async Task<ConfirmPaymentReservationResult> Handle(ConfirmReservationPaymentCommand request, CancellationToken cancellationToken)
        {
            var reservation = await dbContext.Reservations.FindAsync(new object[] { request.ReservationId }, cancellationToken);

            if (reservation is null)
                throw new Exception("Reservation not found.");

            try
            {
                reservation.MarkAsPaid(request.Amount, request.PaymentMethod);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            await dbContext.SaveChangesAsync(cancellationToken);

            return new ConfirmPaymentReservationResult(true);
        }
    }
}
