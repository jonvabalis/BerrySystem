using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Statistics;

public class GetYearsWithDataQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetYearsWithDataQuery, List<int>>
{
    public async Task<List<int>> Handle(GetYearsWithDataQuery query, CancellationToken cancellationToken)
    {
        // assuming every year will have a harvest
        var years = await berrySystemDbContext.Harvests
            .AsNoTracking()
            .Include(h => h.BerryType)
            .Where(h => h.BerryType.Id == query.BerryTypeId)
            .Select(h => h.EventTime.Year)
            .Distinct()
            .ToListAsync(cancellationToken);

        return years;
    }
}