using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.BerryType;

public class GetByNameBerryTypeQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetByNameBerryTypeQuery, Domain.Entities.BerryType>
{
    public async Task<Domain.Entities.BerryType> Handle(GetByNameBerryTypeQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.BerryTypes
            .SingleOrDefaultAsync(berryType => berryType.Type == request.BerryType, cancellationToken)
               ?? throw new NullReferenceException($"No berryType \"{request.BerryType}\" found or multiple instances found");
    }
}