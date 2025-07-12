using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservation.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Data.Configuration
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation.Reservations.Models.Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservations.Models.Reservation> builder)
        {
            builder.Property(O => O.Status).HasConversion(
                OStatus => OStatus.ToString(),
                OStatus => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), OStatus));
        }
    }
}
