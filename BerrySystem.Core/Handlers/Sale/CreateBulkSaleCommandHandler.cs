using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Sale;

public class CreateBulkSaleCommandHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<CreateBulkSaleCommand, Unit>
{
    public async Task<Unit> Handle(CreateBulkSaleCommand request, CancellationToken cancellationToken)
    {
        List<Domain.Entities.Sale> sales = [];
        foreach (var sale in request.Sales)
        {
            //TODO refactor for validation
            var berryType = await berrySystemDbContext.BerryTypes
                .FindAsync([sale.BerryTypeId], cancellationToken);
            
            if (berryType == null)
                throw new Exception($"BerryType with ID {sale.BerryTypeId} not found.");

            Domain.Entities.BerryKind? berryKind = null;
            if (sale.BerryKindId.HasValue)
            {
                berryKind = await berrySystemDbContext.BerryKinds
                    .FindAsync([sale.BerryKindId.Value], cancellationToken);

                if (berryKind == null)
                    throw new Exception($"BerryKind with ID {sale.BerryKindId} was not found.");
            }
            
            var saleEntity = new Domain.Entities.Sale
            {
                Kilograms = sale.Kilograms,
                PricePerKilo = sale.PricePerKilo,
                TotalPrice = sale.TotalPrice,
                EmployeeId = sale.EmployeeId,
                SaleType = sale.SaleType,
                EventTime = sale.EventTime ?? DateTime.UtcNow,
                BerryType = berryType,
                BerryKind = berryKind,
            };
            
            sales.Add(saleEntity);
        }

        await berrySystemDbContext.Sales.AddRangeAsync(sales, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}