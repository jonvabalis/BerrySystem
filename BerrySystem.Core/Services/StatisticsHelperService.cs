using System.Linq.Expressions;
using BerrySystem.Core.Services.Interfaces;
using BerrySystem.Domain.Dtos;
using BerrySystem.Domain.Utilities;
using BerrySystem.Domain.Utilities.TimeSetting;
using BerrySystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Services;

public class StatisticsHelperService(BerrySystemDbContext berrySystemDbContext) : IStatisticsHelperService
{
    public async Task<CollectionStatisticsDto> CollectionStatisticsByFilter(Expression<Func<Domain.Entities.Harvest, bool>> harvestFilter,
        Expression<Func<Domain.Entities.Sale, bool>> saleFilter,
        TimeSetting timeSettingType,
        DateTime requestDate,
        CancellationToken cancellationToken)
    {
        var harvestDataSum = new Dictionary<int, HarvestsSum>();
        await foreach (var harvest in berrySystemDbContext.Harvests
                           .Where(harvestFilter)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            var harvestTime = harvest.EventTime;
            harvestDataSum.TryAdd(timeSettingType.SelectByType(harvestTime), new HarvestsSum(0));
            harvestDataSum[timeSettingType.SelectByType(harvestTime)].Kilograms += harvest.Kilograms;
        }

        var saleDataSum = new Dictionary<int, SalesSum>();
        await foreach (var sale in berrySystemDbContext.Sales
                           .Where(saleFilter)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            var saleTime = sale.EventTime;
            saleDataSum.TryAdd(timeSettingType.SelectByType(saleTime), new SalesSum(0, 0));
            saleDataSum[timeSettingType.SelectByType(saleTime)].Kilograms += sale.Kilograms;
            saleDataSum[timeSettingType.SelectByType(saleTime)].TotalPrice += sale.TotalPrice;
        }

        var collectionStatistics = new Dictionary<int, CollectionStatisticsLine>();
        var collectionStatisticsSum = new CollectionStatisticsLine(0, 0, 0);

        foreach (var key in harvestDataSum.Keys.Union(saleDataSum.Keys))
        {
            var harvestsSum = harvestDataSum.TryGetValue(key, out var harvestsSumTemp) ? harvestsSumTemp : new HarvestsSum(0);
            var salesSum = saleDataSum.TryGetValue(key, out var salesSumTemp) ? salesSumTemp : new SalesSum(0, 0);

            collectionStatistics[key] =
                new CollectionStatisticsLine(harvestsSum.Kilograms, salesSum.Kilograms, salesSum.TotalPrice);

            collectionStatisticsSum.HarvestKilograms += harvestsSum.Kilograms;
            collectionStatisticsSum.SaleKilograms += salesSum.Kilograms;
            collectionStatisticsSum.SaleTotalPrice += salesSum.TotalPrice;
        }

        collectionStatistics = timeSettingType.TableFormat(collectionStatistics, requestDate);

        return new CollectionStatisticsDto
        {
            Data = collectionStatistics,
            Sum = collectionStatisticsSum
        };
    }

    public async Task<CostStatisticsDto> CostStatisticsByFilter(
        Expression<Func<Domain.Entities.Cost, bool>> costFilter,
        TimeSetting timeSettingType,
        DateTime requestDate,
        CancellationToken cancellationToken)
    {
        var costDataSum = new Dictionary<int, CostsSum>();
        var totalCostStatisticsSum = new CostsSum();
        await foreach (var cost in berrySystemDbContext.Costs
                           .Where(costFilter)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            //TODO add eventTime to cost
            var costTime = cost.CreatedAt;
            costDataSum.TryAdd(timeSettingType.SelectByType(costTime), new CostsSum(0));
            costDataSum[timeSettingType.SelectByType(costTime)].Costs += cost.Price;

            totalCostStatisticsSum.Costs += cost.Price;
        }

        costDataSum = timeSettingType.TableFormat(costDataSum, requestDate);

        return new CostStatisticsDto
        {
            Data = costDataSum,
            Sum = totalCostStatisticsSum
        };
    }
}