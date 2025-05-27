using BerrySystem.Core.Queries;
using BerrySystem.Domain.Dtos;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.BerryType;

public class GetByNameBerryTypeQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetByNameBerryTypeQuery, GetAllBerryTypeDto>
{
    public async Task<GetAllBerryTypeDto> Handle(GetByNameBerryTypeQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.BerryTypes
                   .Where(bt => bt.Type == request.BerryType)
                   .Select(bt => new GetAllBerryTypeDto(bt.Id, bt.Type))
                   .SingleOrDefaultAsync(cancellationToken) 
               ?? throw new InvalidOperationException($"No berryType \"{request.BerryType}\" found");
    }
}