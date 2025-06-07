using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Harvest;

public class DeleteHarvestCommandHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<DeleteHarvestCommand, bool>
{
    public async Task<bool> Handle(DeleteHarvestCommand command, CancellationToken cancellationToken)
    {
        var harvest = await berrySystemDbContext.Harvests.FindAsync([command.HarvestId], cancellationToken);

        if (harvest is null)
            throw new Exception($"Harvest with id {command.HarvestId} not found");

        berrySystemDbContext.Harvests.Remove(harvest);

        var result = await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}