using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Harvest;

public class GetAllHarvestQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetAllHarvestsQuery, List<Domain.Entities.Harvest>>
{
    public async Task<List<Domain.Entities.Harvest>> Handle(GetAllHarvestsQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.Harvests.ToListAsync(cancellationToken);
    }
}