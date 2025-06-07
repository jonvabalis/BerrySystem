using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Harvest;

public class UpdateHarvestCommandHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<UpdateHarvestCommand, bool>
{
    public async Task<bool> Handle(UpdateHarvestCommand command, CancellationToken cancellationToken)
    {
        var harvest = await berrySystemDbContext.Harvests
            .Include(h => h.BerryKind)
            .FirstOrDefaultAsync(h => h.Id == command.HarvestId, cancellationToken);

        if (harvest is null)
            throw new Exception($"Harvest with id {command.HarvestId} not found");
     
        var updateHappened = false;
        
        if (command.EmployeeId != harvest.EmployeeId)
        {
            harvest.EmployeeId = command.EmployeeId;
            updateHappened = true;
        }
        
        if (command.BerryKindId != harvest.BerryKind?.Id)
        {
            if (command.BerryKindId == null)
            {
                harvest.BerryKind = null;
            }
            else
            {
                var berryKind = await berrySystemDbContext.BerryKinds
                    .FindAsync([command.BerryKindId], cancellationToken);

                if (berryKind == null)
                    throw new Exception($"BerryType with id {command.BerryKindId} was not found.");
                    
                harvest.BerryKind = berryKind;
            }
            
            updateHappened = true;
        }
        
        if (!command.Kilograms.Equals(harvest.Kilograms))
        {
            harvest.Kilograms = command.Kilograms;
            updateHappened = true;
        }

        if (updateHappened)
        {
            harvest.LastModifiedAt = DateTime.UtcNow;
        }
        
        var result = await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}