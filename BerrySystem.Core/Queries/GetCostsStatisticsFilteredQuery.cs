using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetCostsStatisticsFilteredQuery : IRequest<CostStatisticsDto>
{
    public int? Year { get; set; }
    public int? Month { get; set; }
}