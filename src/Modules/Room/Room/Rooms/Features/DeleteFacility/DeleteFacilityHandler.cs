using Room.Data;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Room.Rooms.Features.DeleteFacility
{
    public record DeleteFacilityCommand(Guid Id):ICommand<DeleteFacilityResult>;

    public record DeleteFacilityResult(bool IsSuccess);

    public class DeleteFacilityHandler(RoomDbContext dbContext) : ICommandHandler<DeleteFacilityCommand, DeleteFacilityResult>
    {
        public async Task<DeleteFacilityResult> Handle(DeleteFacilityCommand request, CancellationToken cancellationToken)
        {
            var facility = await dbContext.Facilities.FindAsync(request.Id, cancellationToken);

            if (facility == null)
            {
                throw new Exception($"Facility not found {request.Id}");
            }

            facility.IsDeleted = true;

            dbContext.Facilities.Update(facility);

            await dbContext.SaveChangesAsync();

            return new DeleteFacilityResult(true);
        }
    }
}
