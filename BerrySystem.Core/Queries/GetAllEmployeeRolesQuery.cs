using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllEmployeeRolesQuery : IRequest<List<RoleDto>>
{
    public required Guid EmployeeId { get; set; }
}