using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Sale;

public class DeleteSaleCommandHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<DeleteSaleCommand, bool>
{
    public async Task<bool> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {
        var sales = await berrySystemDbContext.Sales.FindAsync([command.SaleId], cancellationToken);

        if (sales is null)
            throw new Exception($"Sale with id {command.SaleId} not found");

        berrySystemDbContext.Sales.Remove(sales);

        var result = await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}