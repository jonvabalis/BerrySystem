using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;
using BerrySystem.Domain.Entities;

namespace BerrySystem.Core.Handlers.Harvest;

public class CreateHarvestCommandHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<CreateHarvestCommand, Guid>
{
    public async Task<Guid> Handle(CreateHarvestCommand request, CancellationToken cancellationToken)
    {
        var berryType = await berrySystemDbContext.BerryTypes
            .FindAsync([request.BerryTypeId], cancellationToken);

        if (berryType == null)
            throw new Exception($"BerryType with ID {request.BerryTypeId} not found.");

        BerryKind? berryKind = null;
        if (request.BerryKindId.HasValue)
        {
            berryKind = await berrySystemDbContext.BerryKinds
                .FindAsync([request.BerryKindId.Value], cancellationToken);

            if (berryKind == null)
                throw new Exception($"BerryKind with ID {request.BerryKindId} was not found.");
        }
        
        //TODO: Validation
        // if (berryKind != null && berryKind.BerryType.Id != berryType.Id)
        // {
        //     throw new Exception("The specified BerryKind does not belong to the given BerryType.");
        // }
        
        var harvest = new Domain.Entities.Harvest
        {
            Kilograms = request.Kilograms,
            EmployeeId = request.EmployeeId,
            EventTime = DateTime.UtcNow,
            BerryType = berryType,
            BerryKind = berryKind,
        };
        
        await berrySystemDbContext.Harvests.AddAsync(harvest, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);
        
        return harvest.Id;
    }
}