using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Employee;

public class CreateEmployeeCommandHandler(BerrySystemDbContext berrySystemDbContext)
: IRequestHandler<CreateEmployeeCommand, Guid>
{
    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Domain.Entities.Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Birthday = request.Birthday,
        };

        await berrySystemDbContext.Employees.AddAsync(employee, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}