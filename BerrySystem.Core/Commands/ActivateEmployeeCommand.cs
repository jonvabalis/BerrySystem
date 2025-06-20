using MediatR;

namespace BerrySystem.Core.Commands;

public class ActivateEmployeeCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
}