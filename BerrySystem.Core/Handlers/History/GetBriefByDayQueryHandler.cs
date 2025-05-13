using BerrySystem.Core.Queries;
using BerrySystem.Domain.Dtos;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.History;

public class GetBriefByDayQueryHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<GetBriefByDayQuery, BriefStatisticDto>
{
    public async Task<BriefStatisticDto> Handle(GetBriefByDayQuery request, CancellationToken cancellationToken)
    {
        var briefStatistic = new BriefStatisticDto();
        await foreach (var harvest in berrySystemDbContext.Harvests
                           .Where(harvest => DateOnly.FromDateTime(harvest.EventTime) == request.Date)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            briefStatistic.HarvestedCount += harvest.Kilograms;
        }
        
        await foreach (var sale in berrySystemDbContext.Sales
                           .Where(sale => DateOnly.FromDateTime(sale.EventTime) == request.Date)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            briefStatistic.SoldCount += sale.Kilograms;
            briefStatistic.SoldSum += sale.TotalPrice;
        }

        return briefStatistic;
    }
}