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

namespace Room.Rooms.Features.GetAllFacilities
{

    public record GetAllFacilitiesQuery():IQuery<GetAllFacilitiesResult>;

    public record GetAllFacilitiesResult(IEnumerable<FacilityDto> Facilities);

    public class GetAllFacilitiesHandler(RoomDbContext dbContext) : IQueryHandler<GetAllFacilitiesQuery, GetAllFacilitiesResult>
    {
        public async Task<GetAllFacilitiesResult> Handle(GetAllFacilitiesQuery request, CancellationToken cancellationToken)
        {
            var facilities = await dbContext.Facilities.AsNoTracking().OrderBy(f => f.Name).ToListAsync(cancellationToken);

            var facilitiesDto = facilities.Adapt<List<FacilityDto>>();

            return new GetAllFacilitiesResult(facilitiesDto);
        }
    }
}
