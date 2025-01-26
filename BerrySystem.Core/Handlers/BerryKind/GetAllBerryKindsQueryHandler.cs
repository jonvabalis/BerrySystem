using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.BerryKind;

public class GetAllBerryKindsQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetAllBerryKindsQuery, List<Domain.Entities.BerryKind>>
{
    public async Task<List<Domain.Entities.BerryKind>> Handle(GetAllBerryKindsQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.BerryKinds
            .Where(berryKind => berryKind.BerryTypeId == request.BerryTypeId)
            .ToListAsync(cancellationToken);
    }
}