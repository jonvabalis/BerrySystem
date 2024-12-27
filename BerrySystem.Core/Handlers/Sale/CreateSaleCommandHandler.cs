using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Sale;

public class CreateSaleCommandHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<CreateSaleCommand, Guid>
{
    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Domain.Entities.Sale
        {
            Kilograms = request.Kilograms,
            PricePerKilo = request.PricePerKilo,
            TotalPrice = request.TotalPrice,
            EmployeeId = request.EmployeeId,
            SaleType = request.SaleType,
            EventTime = DateTime.UtcNow,
        };
        
        await berrySystemDbContext.Sales.AddAsync(sale, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);
        
        return sale.Id;
    }
}