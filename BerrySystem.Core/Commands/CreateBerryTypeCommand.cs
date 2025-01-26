using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateBerryTypeCommand : IRequest<Guid>
{
    public string Type { get; set; } = string.Empty;
}