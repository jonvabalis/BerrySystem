using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateRoleCommand : IRequest<Guid>
{
    public required string Name { get; set; }
}