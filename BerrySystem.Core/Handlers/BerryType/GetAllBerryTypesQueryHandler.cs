using BerrySystem.Core.Queries;
using BerrySystem.Domain.Dtos;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.BerryType;

public class GetAllBerryTypesQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetAllBerryTypesQuery, List<GetAllBerryTypeDto>>
{
    public async Task<List<GetAllBerryTypeDto>> Handle(GetAllBerryTypesQuery request, CancellationToken cancellationToken)
    {
        var berryTypes = await berrySystemDbContext.BerryTypes.ToListAsync(cancellationToken);

        return berryTypes.Select(berryType => new GetAllBerryTypeDto(berryType.Id, berryType.Type)).ToList();
    }
}