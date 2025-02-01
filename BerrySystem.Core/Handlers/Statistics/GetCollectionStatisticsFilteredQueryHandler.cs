using System.Linq.Expressions;
using BerrySystem.Core.Queries;
using BerrySystem.Core.Services.Interfaces;
using BerrySystem.Domain.Types;
using MediatR;

namespace BerrySystem.Core.Handlers.Statistics;

public class GetCollectionStatisticsFilteredQueryHandler(IStatisticsHelperService statisticsHelperService)
    : IRequestHandler<GetCollectionStatisticsFilteredQuery, Domain.Dtos.CollectionStatisticsDto>
{
    public async Task<Domain.Dtos.CollectionStatisticsDto> Handle(GetCollectionStatisticsFilteredQuery request, CancellationToken cancellationToken)
    {
        if (request is { Year: not null, Month: not null })
        {
            Expression<Func<Domain.Entities.Harvest, bool>> harvestFilter = harvest => harvest.BerryType.Id == request.BerryTypeId &&
                harvest.CreatedAt.Year == request.Year &&
                harvest.CreatedAt.Month == request.Month;
            Expression<Func<Domain.Entities.Sale, bool>> saleFilter = sale => sale.BerryType.Id == request.BerryTypeId &&
                                                                              sale.CreatedAt.Year == request.Year &&
                                                                              sale.CreatedAt.Month == request.Month;
            
            return await statisticsHelperService.CollectionStatisticsByFilter(harvestFilter, saleFilter, TimeSettingType.Day, cancellationToken);
        }

        if (request.Year is not null)
        {
            Expression<Func<Domain.Entities.Harvest, bool>> harvestFilter = harvest => harvest.BerryType.Id == request.BerryTypeId &&
                harvest.CreatedAt.Year == request.Year;
            Expression<Func<Domain.Entities.Sale, bool>> saleFilter = sale => sale.BerryType.Id == request.BerryTypeId
                                                                              && sale.CreatedAt.Year == request.Year;
            
            return await statisticsHelperService.CollectionStatisticsByFilter(harvestFilter, saleFilter, TimeSettingType.Month, cancellationToken);
        }

        if (request.Month is not null)
        {
            Expression<Func<Domain.Entities.Harvest, bool>> harvestFilter = harvest => harvest.BerryType.Id == request.BerryTypeId &&
                harvest.CreatedAt.Month == request.Month;
            Expression<Func<Domain.Entities.Sale, bool>> saleFilter = sale => sale.BerryType.Id == request.BerryTypeId &&
                                                                              sale.CreatedAt.Month == request.Month;
            
            return await statisticsHelperService.CollectionStatisticsByFilter(harvestFilter, saleFilter, TimeSettingType.Year, cancellationToken);
        }
        
        throw new Exception();
    }

    
}
