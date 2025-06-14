using BerrySystem.Core.Queries;
using BerrySystem.Domain.Dtos;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Statistics;

public class GetCompareByYearStatisticsQueryHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<GetCompareByYearStatisticsQuery, CompareByYearStatisticsDto>
{
    public async Task<CompareByYearStatisticsDto> Handle(GetCompareByYearStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var matchedHarvestData = await MatchHarvests(request, cancellationToken);
        var matchedSaleData = await MatchSales(request, cancellationToken);

        var groupedHarvestDataByMonth = GroupHarvestDataByMonth(matchedHarvestData);
        var groupedSaleDataByMonth = GroupSaleDataByMonth(matchedSaleData);

        return new CompareByYearStatisticsDto
        {
            HarvestKilograms = groupedHarvestDataByMonth,
            SaleKilograms = groupedSaleDataByMonth.Select(g => g.groupedSalesByYear).ToList(),
            SaleRevenue = groupedSaleDataByMonth.Select(g => g.groupedRevenueByYear).ToList(),
        };
    }
    
    private async Task<List<Domain.Entities.Harvest>> MatchHarvests(GetCompareByYearStatisticsQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.Harvests
            .Include(h => h.BerryType)
            .Where(h => h.BerryType.Id == request.BerryTypeId &&
                        request.Years.Any(y => y == h.EventTime.Year))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    private async Task<List<Domain.Entities.Sale>> MatchSales(GetCompareByYearStatisticsQuery request, CancellationToken cancellationToken)
    {
        return await berrySystemDbContext.Sales
            .Include(s => s.BerryType)
            .Where(s => s.BerryType.Id == request.BerryTypeId &&
                        request.Years.Any(y => y == s.EventTime.Year))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    private static List<Dictionary<string, double>> GroupHarvestDataByMonth(List<Domain.Entities.Harvest> matchedHarvestData)
    {
        return matchedHarvestData
            .GroupBy(h => h.EventTime.Month)
            .OrderBy(g => g.Key)
            .Select(mg =>
            {
                var groupedByYear = new Dictionary<string, double>
                {
                    ["time"] = mg.Key,
                };
                
                foreach (var yearGroup in mg.GroupBy(h => h.EventTime.Year))
                {
                    groupedByYear[yearGroup.Key.ToString()] = yearGroup.Sum(h => h.Kilograms);
                }
                return groupedByYear;
            }).ToList();
    }
    
    private static List<(Dictionary<string, double> groupedSalesByYear, Dictionary<string, double> groupedRevenueByYear)> GroupSaleDataByMonth(List<Domain.Entities.Sale> matchedSaleData)
    {
        return matchedSaleData
            .GroupBy(s => s.EventTime.Month)
            .OrderBy(g => g.Key)
            .Select(mg =>
            {
                var groupedSalesByYear = new Dictionary<string, double>
                {
                    ["time"] = mg.Key,
                };
                var groupedRevenueByYear = new Dictionary<string, double>
                {
                    ["time"] = mg.Key,
                };
                
                foreach (var yearGroup in mg.GroupBy(s => s.EventTime.Year))
                {
                    groupedSalesByYear[yearGroup.Key.ToString()] = yearGroup.Sum(s => s.Kilograms);
                    groupedRevenueByYear[yearGroup.Key.ToString()] = yearGroup.Sum(s => s.TotalPrice);
                }
                return (groupedSalesByYear, groupedRevenueByYear);
            }).ToList();
    }
}