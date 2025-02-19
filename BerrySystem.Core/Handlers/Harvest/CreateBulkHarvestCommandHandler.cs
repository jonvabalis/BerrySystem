using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Harvest;

public class CreateBulkHarvestCommandHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<CreateBulkHarvestCommand, Unit>
{
    public async Task<Unit> Handle(CreateBulkHarvestCommand request, CancellationToken cancellationToken)
    {
        List<Domain.Entities.Harvest> harvests = [];
        foreach (var harvest in request.Harvests)
        {
            //TODO refactor for validation
            var berryType = await berrySystemDbContext.BerryTypes
                .FindAsync([harvest.BerryTypeId], cancellationToken);
            
            if (berryType == null)
                throw new Exception($"BerryType with ID {harvest.BerryTypeId} not found.");

            Domain.Entities.BerryKind? berryKind = null;
            if (harvest.BerryKindId.HasValue)
            {
                berryKind = await berrySystemDbContext.BerryKinds
                    .FindAsync([harvest.BerryKindId.Value], cancellationToken);

                if (berryKind == null)
                    throw new Exception($"BerryKind with ID {harvest.BerryKindId} was not found.");
            }
            
            var harvestEntity = new Domain.Entities.Harvest
            {
                Kilograms = harvest.Kilograms,
                EmployeeId = harvest.EmployeeId,
                EventTime = DateTime.UtcNow,
                BerryType = berryType,
                BerryKind = berryKind,
            };
            
            harvests.Add(harvestEntity);
        }

        await berrySystemDbContext.Harvests.AddRangeAsync(harvests, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}