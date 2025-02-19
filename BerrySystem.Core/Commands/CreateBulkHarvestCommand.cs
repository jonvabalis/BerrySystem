using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateBulkHarvestCommand : IRequest<Unit>
{
    public required List<CreateHarvestCommand> Harvests { get; set; }
}