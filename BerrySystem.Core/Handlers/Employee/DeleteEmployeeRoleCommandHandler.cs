using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Employee;

public class DeleteEmployeeRoleCommandHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<DeleteEmployeeRoleCommand, Unit>
{
    public async Task<Unit> Handle(DeleteEmployeeRoleCommand request, CancellationToken cancellationToken)
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
        
        if (employee.Roles.All(r => r.Id != request.RoleId))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ("EmployeeRole","Employee doesn't have this role")
            });
        }
        
        employee.Roles.Remove(role);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}