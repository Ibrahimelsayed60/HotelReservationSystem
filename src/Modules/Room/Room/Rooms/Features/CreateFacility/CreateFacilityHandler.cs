using Mapster;
using Room.Data;
using Room.Rooms.Dtos;
using Room.Rooms.Models;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Room.Rooms.Features.CreateFacility
{
    public record CreateFacilityCommand(FacilityDto Facility):ICommand<CreateFacilityResult>;

    public record CreateFacilityResult(Guid Id);

    public class CreateFacilityHandler(RoomDbContext dbContext) : ICommandHandler<CreateFacilityCommand, CreateFacilityResult>
    {
        public async Task<CreateFacilityResult> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
        {
            var facility = request.Facility.Adapt<Facilities>();

            dbContext.Facilities.Add(facility);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateFacilityResult(facility.Id);
        }
    }
}
