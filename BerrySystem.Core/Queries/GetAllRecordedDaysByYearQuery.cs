using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllRecordedDaysByYearQuery : IRequest<DateOnly[]>
{
    public int Year { get; set; }
    public Guid BerryTypeId { get; set; }

}