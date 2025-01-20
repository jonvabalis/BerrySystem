using MediatR;

namespace BerrySystem.Core.Queries;

public class GetByIdEmployeeQuery : IRequest<Domain.Entities.Employee>
{
    public Guid EmployeeId { get; set; }
}