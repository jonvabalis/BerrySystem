using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateBerryKindCommand : IRequest<Guid>
{
    public required string Kind { get; set; }
    public Guid BerryTypeId { get; set; }
}