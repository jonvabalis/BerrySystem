using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllEmployeesQuery  : IRequest<List<Domain.Entities.Employee>>
{
    
}