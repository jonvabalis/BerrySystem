using MediatR;

namespace BerrySystem.Core.Commands;

public class DeleteEmployeeRoleCommand : IRequest<Unit>
{
    public required Guid EmployeeId { get; set; } 
    public required Guid RoleId { get; set; }
}