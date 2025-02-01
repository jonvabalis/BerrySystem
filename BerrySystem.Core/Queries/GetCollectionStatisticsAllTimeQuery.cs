using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetCollectionStatisticsAllTimeQuery : IRequest<CollectionStatisticsDto>
{
    public Guid BerryTypeId { get; set; }
}