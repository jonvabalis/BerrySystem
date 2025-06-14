using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetCompareByYearStatisticsQuery : IRequest<CompareByYearStatisticsDto>
{
    public Guid BerryTypeId { get; set; }
    public required List<int> Years { get; set; }
}