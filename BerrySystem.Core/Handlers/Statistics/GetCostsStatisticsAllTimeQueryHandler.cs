using System.Linq.Expressions;
using BerrySystem.Core.Queries;
using BerrySystem.Core.Services.Interfaces;
using BerrySystem.Domain.Types;
using MediatR;

namespace BerrySystem.Core.Handlers.Statistics;

public class GetCostsStatisticsAllTimeQueryHandler(IStatisticsHelperService statisticsHelperService)
    : IRequestHandler<GetCostsStatisticsAllTimeQuery, Domain.Dtos.CostStatisticsDto>
{
    public async Task<Domain.Dtos.CostStatisticsDto> Handle(GetCostsStatisticsAllTimeQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Cost, bool>> costFilter = cost => true;
            
        return await statisticsHelperService.CostStatisticsByFilter(costFilter, TimeSettingType.Year, cancellationToken);
    }
}