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
        var harvest = new Domain.Entities.Harvest
        {
            Kilograms = request.Kilograms,
            EmployeeId = request.EmployeeId,
            EventTime = DateTime.UtcNow,
        };
        
        await berrySystemDbContext.Harvests.AddAsync(harvest, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);
        
        return harvest.Id;
    }
}