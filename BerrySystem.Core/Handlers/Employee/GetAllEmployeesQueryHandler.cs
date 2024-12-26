using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Employee;

public class GetAllEmployeesQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetAllEmployeesQuery, List<Domain.Entities.Employee>>
{
    public async Task<List<Domain.Entities.Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.Employees.ToListAsync(cancellationToken);
    }
}