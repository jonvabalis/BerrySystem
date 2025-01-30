using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Cost;

public class GetCostsSumQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetCostsSumQuery, double>
{
    public async Task<double> Handle(GetCostsSumQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.Costs.SumAsync(cost => cost.Price, cancellationToken);
    }
}