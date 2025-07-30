using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Employee;

public class AddEmployeeRoleCommandHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<AddEmployeeRoleCommand, Unit>
{
    public async Task<Unit> Handle(AddEmployeeRoleCommand request, CancellationToken cancellationToken)
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
        
        var role = await berrySystemDbContext.Roles.FindAsync([request.RoleId], cancellationToken);
        
        if (role is null)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ("Role","Role does not exist")
            });
        }
        
        if (employee.Roles.Any(r => r.Id == request.RoleId))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ("EmployeeRole","Employee already has this role")
            });
        }
        
        employee.Roles.Add(role);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}