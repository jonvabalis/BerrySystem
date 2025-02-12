using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Cost;

public class CreateCostCommandHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<CreateCostCommand, Guid>
{
    public async Task<Guid> Handle(CreateCostCommand request, CancellationToken cancellationToken)
    {
        var cost = new Domain.Entities.Cost
        {
            Price = request.Price,
        };

        await berrySystemDbContext.Costs.AddAsync(cost, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return cost.Id;
    }
}