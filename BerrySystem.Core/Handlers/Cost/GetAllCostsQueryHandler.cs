using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Cost;

public class GetAllCostsQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetAllCostsQuery, List<Domain.Entities.Cost>>
{
    public async Task<List<Domain.Entities.Cost>> Handle(GetAllCostsQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.Costs.ToListAsync(cancellationToken);
    }
}