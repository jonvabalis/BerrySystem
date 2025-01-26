using BerrySystem.Core.Commands;
using BerrySystem.Domain.Entities;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Sale;

public class CreateSaleCommandHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<CreateSaleCommand, Guid>
{
    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var berryType = await berrySystemDbContext.BerryTypes
            .FindAsync([request.BerryTypeId], cancellationToken);

        if (berryType == null)
            throw new Exception($"BerryType with ID {request.BerryTypeId} not found.");

        Domain.Entities.BerryKind? berryKind = null;
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
        
        var sale = new Domain.Entities.Sale
        {
            Kilograms = request.Kilograms,
            PricePerKilo = request.PricePerKilo,
            TotalPrice = request.TotalPrice,
            EmployeeId = request.EmployeeId,
            SaleType = request.SaleType,
            EventTime = DateTime.UtcNow,
            BerryType = berryType,
            BerryKind = berryKind,
        };
        
        await berrySystemDbContext.Sales.AddAsync(sale, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);
        
        return sale.Id;
    }
}