using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetBriefByDayQuery : IRequest<HistoryBriefStatisticsDto>
{
    public DateOnly Date { get; set; }
    public Guid BerryTypeId { get; set; }
}