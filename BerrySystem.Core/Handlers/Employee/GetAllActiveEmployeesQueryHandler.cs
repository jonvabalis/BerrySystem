using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Employee;

public class GetAllActiveEmployeesQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetAllActiveEmployeesQuery, List<Domain.Entities.Employee>>
{
    public async Task<List<Domain.Entities.Employee>> Handle(GetAllActiveEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.Employees.Where(e => e.IsActive == request.IsActive).ToListAsync(cancellationToken);
    }
}