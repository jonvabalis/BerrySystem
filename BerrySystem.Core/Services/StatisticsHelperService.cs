using System.Linq.Expressions;
using BerrySystem.Core.Extensions;
using BerrySystem.Core.Services.Interfaces;
using BerrySystem.Domain.Dtos;
using BerrySystem.Domain.Types;
using BerrySystem.Domain.Utilities;
using BerrySystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Services;

public class StatisticsHelperService(BerrySystemDbContext berrySystemDbContext) : IStatisticsHelperService
{
    public async Task<Domain.Dtos.CollectionStatisticsDto> CollectionStatisticsByFilter(Expression<Func<Domain.Entities.Harvest, bool>> harvestFilter,
        Expression<Func<Domain.Entities.Sale, bool>> saleFilter,
        TimeSettingType timeSettingType,
        CancellationToken cancellationToken)
    {
        var timeSettingSelector = DateTimeExtensions.TimeSettingSelector(timeSettingType);
        
        var harvestDataSum = new Dictionary<int, HarvestsSum>();
        await foreach (var harvest in berrySystemDbContext.Harvests
                           .Where(harvestFilter)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            //TODO use eventTime instead of createdAt
            harvestDataSum.TryAdd(timeSettingSelector(harvest.CreatedAt), new HarvestsSum(0));
            harvestDataSum[timeSettingSelector(harvest.CreatedAt)].Kilograms += harvest.Kilograms;
        }
        
        var saleDataSum = new Dictionary<int, SalesSum>(); ;
        await foreach (var sale in berrySystemDbContext.Sales
                           .Where(saleFilter)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            //TODO use eventTime instead of createdAt
            saleDataSum.TryAdd(timeSettingSelector(sale.CreatedAt), new SalesSum(0, 0));
            saleDataSum[timeSettingSelector(sale.CreatedAt)].Kilograms += sale.Kilograms;
            saleDataSum[timeSettingSelector(sale.CreatedAt)].TotalPrice += sale.TotalPrice;
        }
        
        var collectionStatistics = new Dictionary<int, CollectionStatisticsLine>();
        var collectionStatisticsSum = new CollectionStatisticsLine(0, 0 , 0);
        
        foreach (var key in harvestDataSum.Keys.Union(saleDataSum.Keys))
        {
            var harvestsSum = harvestDataSum.TryGetValue(key, out var harvestsSumTemp) ? harvestsSumTemp : new HarvestsSum(0);
            var salesSum = saleDataSum.TryGetValue(key,  out var salesSumTemp) ? salesSumTemp : new SalesSum(0, 0);

            collectionStatistics[key] =
                new CollectionStatisticsLine(harvestsSum.Kilograms, salesSum.Kilograms, salesSum.TotalPrice);
            
            collectionStatisticsSum.HarvestKilograms += harvestsSum.Kilograms;
            collectionStatisticsSum.SaleKilograms += salesSum.Kilograms;
            collectionStatisticsSum.SaleTotalPrice += salesSum.TotalPrice;
        }

        return new CollectionStatisticsDto
        {
            Data = collectionStatistics,
            Sum = collectionStatisticsSum
        };
    }

    public async Task<Domain.Dtos.CostStatisticsDto> CostStatisticsByFilter(
        Expression<Func<Domain.Entities.Cost, bool>> costFilter,
        TimeSettingType timeSettingType,
        CancellationToken cancellationToken)
    {
        var timeSettingSelector = DateTimeExtensions.TimeSettingSelector(timeSettingType);
        
        var costDataSum = new Dictionary<int, CostsSum>();
        var totalCostStatisticsSum = 0.0;
        await foreach (var cost in berrySystemDbContext.Costs
                           .Where(costFilter)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            //TODO use eventTime instead of createdAt ?
            costDataSum.TryAdd(timeSettingSelector(cost.CreatedAt), new CostsSum(0));
            costDataSum[timeSettingSelector(cost.CreatedAt)].Costs += cost.Price;

            totalCostStatisticsSum += cost.Price;
        }
        
        //TimeSettin
        var costStatistics = new Dictionary<int, double>();
        
        foreach (var key in costDataSum.Keys)
        {
           costStatistics[key] = costDataSum[key].Costs;
        }

        return new CostStatisticsDto
        {
            Data = costStatistics,
            Sum = totalCostStatisticsSum
        };
    }
}