using System.Linq.Expressions;
using BerrySystem.Core.Queries;
using BerrySystem.Core.Services.Interfaces;
using BerrySystem.Domain.Utilities.TimeSetting;
using MediatR;

namespace BerrySystem.Core.Handlers.Statistics;

public class GetCostsStatisticsFilteredQueryHandler(IStatisticsHelperService statisticsHelperService)
    : IRequestHandler<GetCostsStatisticsFilteredQuery, Domain.Dtos.CostStatisticsDto>
{
    public async Task<Domain.Dtos.CostStatisticsDto> Handle(GetCostsStatisticsFilteredQuery request, CancellationToken cancellationToken)
    {
        if (request is { Year: not null, Month: not null })
        {
            Expression<Func<Domain.Entities.Cost, bool>> costFilter = cost => cost.CreatedAt.Year == request.Year &&
                cost.CreatedAt.Month == request.Month;

            return await statisticsHelperService.CostStatisticsByFilter(costFilter, new DayTimeSetting(),
                new DateTime(request.Year ?? 1, request.Month ?? 1, 1), cancellationToken);
        }

        if (request.Year is not null)
        {
            Expression<Func<Domain.Entities.Cost, bool>> costFilter = cost => cost.CreatedAt.Year == request.Year;

            return await statisticsHelperService.CostStatisticsByFilter(costFilter, new MonthTimeSetting(),
                new DateTime(), cancellationToken);
        }

        if (request.Month is not null)
        {
            Expression<Func<Domain.Entities.Cost, bool>> costFilter = cost => cost.CreatedAt.Month == request.Month;

            return await statisticsHelperService.CostStatisticsByFilter(costFilter, new YearTimeSetting(), new DateTime(), cancellationToken);
        }

        throw new Exception();
    }
}