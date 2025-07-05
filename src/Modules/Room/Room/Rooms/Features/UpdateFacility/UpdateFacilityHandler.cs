using Room.Data;
using Room.Rooms.Dtos;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Room.Rooms.Features.UpdateFacility
{
    public record UpdateFacilityCommand(FacilityDto Facility): ICommand<UpdateFacilityResult>;

    public record UpdateFacilityResult(bool IsSuccess);

    public class UpdateFacilityHandler(RoomDbContext dbContext) : ICommandHandler<UpdateFacilityCommand, UpdateFacilityResult>
    {
        public async Task<UpdateFacilityResult> Handle(UpdateFacilityCommand request, CancellationToken cancellationToken)
        {
            var facility = await dbContext.Facilities.FindAsync(request.Facility.Id, cancellationToken);

            if(facility == null)
            {
                throw new Exception($"Facility not found {request.Facility.Id}");
            }

            facility.Name = request.Facility.Name;

            dbContext.Facilities.Update(facility);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateFacilityResult(true);
        }
    }
}
