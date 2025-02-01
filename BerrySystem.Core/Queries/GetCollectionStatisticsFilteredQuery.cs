using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetCollectionStatisticsFilteredQuery : IRequest<CollectionStatisticsDto>
{
    public Guid BerryTypeId { get; set; }
    public int? Year { get; set; }
    public int? Month { get; set; }
}