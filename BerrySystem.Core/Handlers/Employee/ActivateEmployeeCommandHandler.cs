using BerrySystem.Core.Commands;
using BerrySystem.Core.Validation;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Employee;

public class ActivateEmployeeCommandHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<ActivateEmployeeCommand, bool>
{
    public async Task<bool> Handle(ActivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await berrySystemDbContext.Employees.FindAsync([request.EmployeeId], cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException("Employee", $"{request.EmployeeId}");
        }

        employee.IsActive = true;

        await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}