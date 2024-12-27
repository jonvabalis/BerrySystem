using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Sale;

public class GetAllSalesQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetAllSalesQuery, List<Domain.Entities.Sale>>
{
    public async Task<List<Domain.Entities.Sale>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.Sales.ToListAsync(cancellationToken);
    }
}