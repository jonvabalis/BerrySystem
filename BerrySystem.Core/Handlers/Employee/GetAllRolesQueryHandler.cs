using BerrySystem.Core.Queries;
using BerrySystem.Domain.Dtos;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Employee;

public class GetAllRolesQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetAllRolesQuery, List<RoleDto>>
{
    public async Task<List<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.Roles
            .Select(r => new RoleDto{Id = r.Id, Name = r.Name})
            .ToListAsync(cancellationToken);
    }
}