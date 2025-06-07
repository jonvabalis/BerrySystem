using MediatR;

namespace BerrySystem.Core.Commands;

public class DeleteHarvestCommand : IRequest<bool>
{
    public Guid HarvestId { get; set; }
}