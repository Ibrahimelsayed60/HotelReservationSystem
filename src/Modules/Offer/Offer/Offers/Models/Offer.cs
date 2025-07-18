using Mapster;
using Offer.Offers.Dtos;
using Offer.Offers.Events;
using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offer.Offers.Models
{
    public class Offer : Aggregate<Guid>
    {
        public string Title { get; set; }                     // e.g. "Summer Sale"
        public string Description { get; set; }               // e.g. "Get 20% off..."
        public decimal DiscountPercentage { get; set; }       // e.g. 20.0
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } 

        public ICollection<OfferRoom> OfferRooms { get; set; }

        public ICollection<OfferUsage> OfferUsages { get; set; }

        public Offer()
        {
            IsActive = StartDate <= DateTime.UtcNow && EndDate >= DateTime.UtcNow;
        }

        public static Offer Create(string title, string description, decimal discountPercentage, DateTime start, DateTime end)
        {
            if (discountPercentage <= 0 || discountPercentage > 100)
                throw new Exception("Discount must be between 0 and 100.");

            if (start >= end)
                throw new Exception("End date must be after start date.");

            var offer = new Offer
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                DiscountPercentage = discountPercentage,
                StartDate = start,
                EndDate = end
            };

            offer.AddDomainEvent(new OfferCreatedEvent(offer.Adapt<OfferDto>()));
            return offer;
        }

        public void UpdateDetails(string title, string description, decimal discountPercentage, DateTime start, DateTime end)
        {
            if (discountPercentage <= 0 || discountPercentage > 100)
                throw new Exception("Discount must be between 0 and 100.");

            if (start >= end)
                throw new Exception("End date must be after start date.");

            Title = title;
            Description = description;
            DiscountPercentage = discountPercentage;
            StartDate = start;
            EndDate = end;
        }

        public void Activate()
        {
            if (IsActive)
                return;

            IsActive = true;
            AddDomainEvent(new OfferActivatedEvent(Id, DateTime.UtcNow));
        }

        public void Deactivate()
        {
            if (!IsActive)
                return;

            IsActive = false;
            AddDomainEvent(new OfferDeactivatedEvent(Id, DateTime.UtcNow));
        }

        public void ApplyToRoom(Guid roomId, decimal? customDiscount = null)
        {
            if (OfferRooms.Any(x => x.RoomId == roomId))
                throw new Exception("Room already linked to this offer.");

            OfferRooms.Add(new OfferRoom
            {
                OfferId = this.Id,
                RoomId = roomId,
                CustomDiscountPercentage = customDiscount
            });

            AddDomainEvent(new OfferAppliedToRoomEvent(this.Id, roomId, customDiscount ?? this.DiscountPercentage));
        }

        public void RemoveFromRoom(Guid roomId)
        {
            var offerRoom = OfferRooms.FirstOrDefault(x => x.RoomId == roomId);
            if (offerRoom is null)
                throw new Exception("Room not linked to this offer.");

            OfferRooms.Remove(offerRoom);
            AddDomainEvent(new OfferRemovedFromRoomEvent(this.Id, roomId));
        }

        public bool IsApplicableTo(DateTime checkInDate, DateTime checkOutDate)
        {
            return checkInDate >= StartDate && checkOutDate <= EndDate;
        }

        public decimal GetEffectiveDiscountForRoom(Guid roomId)
        {
            var room = OfferRooms.FirstOrDefault(r => r.RoomId == roomId);
            return room?.CustomDiscountPercentage ?? DiscountPercentage;
        }

    }
}
