using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Employee;

public class GetEmployeeQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetByIdEmployeeQuery, Domain.Entities.Employee>
{
    public async Task<Domain.Entities.Employee> Handle(GetByIdEmployeeQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.Employees.FindAsync([request.EmployeeId], cancellationToken)
               ?? throw new NullReferenceException();
    }
}