using BerrySystem.Core.Queries;
using BerrySystem.Domain.Dtos;
using BerrySystem.Infrastructure;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Employee;

public class GetAllEmployeeRolesQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetAllEmployeeRolesQuery, List<RoleDto>>
{
    public async Task<List<RoleDto>> Handle(GetAllEmployeeRolesQuery request, CancellationToken cancellationToken)
    {
        var employee = await berrySystemDbContext.Employees
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);
        
        if (employee is null)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ("Employee","Employee does not exist")
            });
        }
        
        return employee.Roles
            .Select(r => new RoleDto{Id = r.Id, Name = r.Name})
            .ToList();
    }
}