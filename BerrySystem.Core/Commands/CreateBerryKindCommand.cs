using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateBerryKindCommand : IRequest<Guid>
{
    public string Kind { get; set; } = string.Empty;
    public Guid BerryTypeId { get; set; }
}