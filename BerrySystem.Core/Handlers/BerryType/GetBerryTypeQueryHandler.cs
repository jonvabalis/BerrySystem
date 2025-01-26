using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.BerryType;

public class GetBerryTypeQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetByIdBerryTypeQuery, Domain.Entities.BerryType>
{
    public async Task<Domain.Entities.BerryType> Handle(GetByIdBerryTypeQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.BerryTypes.FindAsync([request.BerryTypeId], cancellationToken)
               ?? throw new NullReferenceException();
    }
}