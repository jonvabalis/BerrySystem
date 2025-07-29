using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Sale;

public class UpdateSaleCommandHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<UpdateSaleCommand, bool>
{
    public async Task<bool> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = await berrySystemDbContext.Sales
            .Include(s => s.BerryKind)
            .FirstOrDefaultAsync(s => s.Id == command.SaleId, cancellationToken);

        if (sale is null)
            throw new Exception($"Harvest with id {command.SaleId} not found");

        var updateHappened = false;

        if (command.EmployeeId != sale.EmployeeId)
        {
            sale.EmployeeId = command.EmployeeId;
            updateHappened = true;
        }

        if (command.BerryKindId != sale.BerryKind?.Id)
        {
            if (command.BerryKindId == null)
            {
                sale.BerryKind = null;
            }
            else
            {
                var berryKind = await berrySystemDbContext.BerryKinds
                    .FindAsync([command.BerryKindId], cancellationToken);

                if (berryKind == null)
                    throw new Exception($"BerryType with id {command.BerryKindId} was not found.");

                sale.BerryKind = berryKind;
            }

            updateHappened = true;
        }

        if (!command.Kilograms.Equals(sale.Kilograms))
        {
            sale.Kilograms = command.Kilograms;
            updateHappened = true;
        }

        if (!command.PricePerKilo.Equals(sale.PricePerKilo))
        {
            sale.PricePerKilo = command.PricePerKilo;
            updateHappened = true;
        }

        if (!command.TotalPrice.Equals(sale.TotalPrice))
        {
            sale.TotalPrice = command.TotalPrice;
            updateHappened = true;
        }

        if (!command.TotalPrice.Equals(sale.TotalPrice))
        {
            sale.TotalPrice = command.TotalPrice;
            updateHappened = true;
        }

        if (!command.SaleType.Equals(sale.SaleType))
        {
            sale.SaleType = command.SaleType;
            updateHappened = true;
        }

        if (updateHappened)
        {
            sale.LastModifiedAt = DateTime.UtcNow;
        }

        var result = await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}