using System.Globalization;
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

        var groupedHarvestDataByYearAndDay = GroupHarvestDataByYearAndDay(matchedHarvestData, request.Years);
        var groupedByYearAndDayByYearAndDay = GroupSaleDataByYearAndDay(matchedSaleData, request.Years);

        const int monthStartDay = 1;
        var monthEndDay = DateTime.DaysInMonth(0001, request.EndMonth);

        var filteredHarvestsByMonth = FilterGroupByMonth(request, monthStartDay, monthEndDay, groupedHarvestDataByYearAndDay);
        var filteredSalesByMonth = FilterGroupByMonth(request, monthStartDay, monthEndDay, groupedByYearAndDayByYearAndDay.groupedSalesByYear);
        var filteredRevenueByMonth = FilterGroupByMonth(request, monthStartDay, monthEndDay, groupedByYearAndDayByYearAndDay.groupedRevenueByYear);

        return new CompareByYearStatisticsDto
        {
            HarvestKilograms = filteredHarvestsByMonth,
            SaleKilograms = filteredSalesByMonth,
            SaleRevenue = filteredRevenueByMonth,
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

    private static List<Dictionary<string, string>> GroupHarvestDataByYearAndDay(List<Domain.Entities.Harvest> matchedHarvestData, List<int> years)
    {
        var harvestsByDate = matchedHarvestData
            .GroupBy(h => h.EventTime.Date)
            .ToDictionary(g => g.Key, g => g.Sum(h => h.Kilograms));

        var startDate = new DateTime(0001, 1, 1); // ignoring leap year
        var endDate = new DateTime(0001, 12, 31);

        var result = new List<Dictionary<string, string>>();
        var cumulativeByYear = years.ToDictionary(y => y, _ => 0.0);

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            var dayKey = date.ToString("MM-dd");
            var harvestRow = new Dictionary<string, string> { ["time"] = dayKey };

            foreach (var year in years)
            {
                var actualDate = new DateTime(year, date.Month, date.Day);
                var dailyValue = harvestsByDate.GetValueOrDefault(actualDate, 0);
                cumulativeByYear[year] += dailyValue;
                harvestRow[year.ToString()] = cumulativeByYear[year].ToString(CultureInfo.InvariantCulture);
            }

            result.Add(harvestRow);
        }

        return result;
    }

    private static (List<Dictionary<string, string>> groupedSalesByYear, List<Dictionary<string, string>> groupedRevenueByYear)
        GroupSaleDataByYearAndDay(List<Domain.Entities.Sale> matchedSaleData, List<int> years)
    {
        var salesByDate = matchedSaleData
            .GroupBy(s => s.EventTime.Date)
            .ToDictionary(g => g.Key, g => new
            {
                Kilograms = g.Sum(s => s.Kilograms),
                Revenue = g.Sum(s => s.TotalPrice)
            });

        var startDate = new DateTime(0001, 1, 1); // ignoring leap year
        var endDate = new DateTime(0001, 12, 31);

        var saleKgResult = new List<Dictionary<string, string>>();
        var saleRevResult = new List<Dictionary<string, string>>();

        var saleCumulative = years.ToDictionary(y => y, _ => 0.0);
        var revenueCumulative = years.ToDictionary(y => y, _ => 0.0);

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            var dayKey = date.ToString("MM-dd");
            var saleKgRow = new Dictionary<string, string> { ["time"] = dayKey };
            var revenueRow = new Dictionary<string, string> { ["time"] = dayKey };

            foreach (var year in years)
            {
                var actualDate = new DateTime(year, date.Month, date.Day);
                if (salesByDate.TryGetValue(actualDate, out var daily))
                {
                    saleCumulative[year] += daily.Kilograms;
                    revenueCumulative[year] += daily.Revenue;
                }

                saleKgRow[year.ToString()] = saleCumulative[year].ToString(CultureInfo.InvariantCulture);
                revenueRow[year.ToString()] = revenueCumulative[year].ToString(CultureInfo.InvariantCulture);
            }

            saleKgResult.Add(saleKgRow);
            saleRevResult.Add(revenueRow);
        }

        return (saleKgResult, saleRevResult);
    }

    private static List<Dictionary<string, string>> FilterGroupByMonth(GetCompareByYearStatisticsQuery request, int monthStartDay, int monthEndDay,
        List<Dictionary<string, string>> groupedHarvestDataByMonth)
    {
        var startKey = $"{request.StartMonth:D2}-{monthStartDay:D2}";
        var endKey = $"{request.EndMonth:D2}-{monthEndDay:D2}";

        var filteredEntries = new List<Dictionary<string, string>>();
        var rangeFound = false;
        foreach (var entry in groupedHarvestDataByMonth)
        {
            if (entry["time"] == startKey)
            {
                rangeFound = true;
            }

            if (rangeFound)
            {
                filteredEntries.Add(entry);

                if (entry["time"] == endKey)
                {
                    break;
                }
            }
        }

        return filteredEntries;
    }
}