using BerrySystem.Core.Queries;
using BerrySystem.Domain.Utilities;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Sale;

public class GetSalesByDateQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetSalesByDateQuery, List<SaleDataLine>>
{
    public async Task<List<SaleDataLine>> Handle(GetSalesByDateQuery request, CancellationToken cancellationToken)
    {
        var startDate = new DateTime(request.SaleDate.Year,
            request.SaleDate.Month,
            request.SaleDate.Day,
            0, 0, 0, 0, 0,
            DateTimeKind.Utc);

        var endDate = startDate.AddDays(1);

        var sales = await berrySystemDbContext.Sales
            .Where(s => s.BerryType.Id == request.BerryTypeId &&
                        s.EventTime >= startDate && s.EventTime < endDate)
            .Select(s => new SaleDataLine
            {
                SaleId = s.Id,
                BerryKindId = s.BerryKind != null ? s.BerryKind.Id : null,
                EventTimeRaw = TimeOnly.FromDateTime(s.EventTime),
                EmployeeId = s.EmployeeId,
                Kilograms = s.Kilograms,
                PricePerKilo = s.PricePerKilo,
                TotalPrice = s.TotalPrice,
                SaleType = s.SaleType
            })
            .ToListAsync(cancellationToken);

        return sales;
    }
}