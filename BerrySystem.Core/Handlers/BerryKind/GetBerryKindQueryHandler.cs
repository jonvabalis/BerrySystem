using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.BerryKind;

public class GetBerryKindQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetByIdBerryKindQuery, Domain.Entities.BerryKind>
{
    public async Task<Domain.Entities.BerryKind> Handle(GetByIdBerryKindQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.BerryKinds.FindAsync([request.BerryKindId], cancellationToken)
               ?? throw new NullReferenceException();
    }
}