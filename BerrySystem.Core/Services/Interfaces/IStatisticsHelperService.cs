using System.Linq.Expressions;
using BerrySystem.Domain.Types;

namespace BerrySystem.Core.Services.Interfaces;

public interface IStatisticsHelperService
{
    public Task<Domain.Dtos.CollectionStatisticsDto> CollectionStatisticsByFilter(
        Expression<Func<Domain.Entities.Harvest, bool>> harvestFilter,
        Expression<Func<Domain.Entities.Sale, bool>> saleFilter,
        TimeSettingType timeSettingType,
        CancellationToken cancellationToken);
    
    public Task<Domain.Dtos.CostStatisticsDto> CostStatisticsByFilter(
        Expression<Func<Domain.Entities.Cost, bool>> costFilter,
        TimeSettingType timeSettingType,
        CancellationToken cancellationToken);
}