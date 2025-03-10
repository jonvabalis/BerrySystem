using BerrySystem.Domain.Utilities;
using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateBulkHarvestCommand : IRequest<Unit>
{
    public required List<BulkHarvest> Harvests { get; set; } 
}