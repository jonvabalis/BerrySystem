using System.Linq.Expressions;
using BerrySystem.Core.Queries;
using BerrySystem.Core.Services.Interfaces;
using BerrySystem.Domain.Types;
using BerrySystem.Domain.Utilities.TimeSetting;
using MediatR;

namespace BerrySystem.Core.Handlers.Statistics;

public class GetCollectionStatisticsAllTimeQueryHandler(IStatisticsHelperService statisticsHelperService)
    : IRequestHandler<GetCollectionStatisticsAllTimeQuery, Domain.Dtos.CollectionStatisticsDto>
{
    public async Task<Domain.Dtos.CollectionStatisticsDto> Handle(GetCollectionStatisticsAllTimeQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Harvest, bool>> harvestFilter = harvest =>
            harvest.BerryType.Id == request.BerryTypeId;
        Expression<Func<Domain.Entities.Sale, bool>> saleFilter = sale => sale.BerryType.Id == request.BerryTypeId;

        return await statisticsHelperService.CollectionStatisticsByFilter(harvestFilter, saleFilter, new YearTimeSetting(), new DateTime(), cancellationToken);
    }
}

