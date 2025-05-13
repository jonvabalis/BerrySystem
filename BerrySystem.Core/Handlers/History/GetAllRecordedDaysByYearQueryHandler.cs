using BerrySystem.Core.Queries;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.History;

public class GetAllRecordedDaysByYearQueryHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<GetAllRecordedDaysByYearQuery, DateOnly[]>
{
    public async Task<DateOnly[]> Handle(GetAllRecordedDaysByYearQuery request, CancellationToken cancellationToken)
    {
        var recordedDays = new HashSet<DateOnly>();
        await foreach (var harvest in berrySystemDbContext.Harvests
                           .Where(harvest => harvest.EventTime.Year == request.Year)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            var date = DateOnly.FromDateTime(harvest.EventTime);
            recordedDays.Add(date);
        }
        
        await foreach (var sale in berrySystemDbContext.Sales
                           .Where(sale => sale.EventTime.Year == request.Year)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            var date = DateOnly.FromDateTime(sale.EventTime);
            recordedDays.Add(date);
        }

        return recordedDays.ToArray();
    }
}