using BerrySystem.Core.Queries;
using BerrySystem.Domain.Utilities;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Harvest;

public class GetHarvestsByDateQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetHarvestsByDateQuery, List<HarvestDataLine>>
{
    public async Task<List<HarvestDataLine>> Handle(GetHarvestsByDateQuery request, CancellationToken cancellationToken)
    {
        var startDate = new DateTime(request.HarvestDate.Year, 
            request.HarvestDate.Month, 
            request.HarvestDate.Day, 
            0, 0, 0, 0, 0, 
            DateTimeKind.Utc);

        var endDate = startDate.AddDays(1);
        
        var harvests = await berrySystemDbContext.Harvests
            .Where(h => h.BerryType.Id == request.BerryTypeId && 
                        h.EventTime >= startDate && h.EventTime < endDate)
            .Select(h => new HarvestDataLine
            {
                HarvestId = h.Id,
                BerryKindId = h.BerryKind != null ? h.BerryKind.Id : null,
                EventTimeRaw = TimeOnly.FromDateTime(h.EventTime),
                EmployeeId = h.EmployeeId,
                Kilograms = h.Kilograms,
            })
            .ToListAsync(cancellationToken);

        return harvests;
    }
}