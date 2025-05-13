using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetBriefByDayQuery : IRequest<BriefStatisticDto>
{
    public DateOnly Date { get; set; }
}