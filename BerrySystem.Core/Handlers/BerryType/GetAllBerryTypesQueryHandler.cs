using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.BerryType;

public class GetAllBerryTypesQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetAllBerryTypesQuery, List<Domain.Entities.BerryType>>
{
    public async Task<List<Domain.Entities.BerryType>> Handle(GetAllBerryTypesQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.BerryTypes.ToListAsync(cancellationToken);
    }
}