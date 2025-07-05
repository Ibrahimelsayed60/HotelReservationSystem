using Mapster;
using Microsoft.EntityFrameworkCore;
using Room.Data;
using Room.Rooms.Dtos;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room.Rooms.Features.GetFacilityById
{
    public record GetFacilityByIdQuery(Guid Id):IQuery<GetFacilityByIdResult>;

    public record GetFacilityByIdResult(FacilityDto Facility);

    public class GetFacilityByIdHandler(RoomDbContext dbContext) : IQueryHandler<GetFacilityByIdQuery, GetFacilityByIdResult>
    {
        public async Task<GetFacilityByIdResult> Handle(GetFacilityByIdQuery request, CancellationToken cancellationToken)
        {
            var facility = await dbContext.Facilities.AsNoTracking().SingleOrDefaultAsync(f => f.Id == request.Id);

            if (facility == null)
            {
                throw new Exception($"Facility not found {request.Id}");
            }

            var facilityDto = facility.Adapt<FacilityDto>();

            return new GetFacilityByIdResult(facilityDto);
        }
    }
}
