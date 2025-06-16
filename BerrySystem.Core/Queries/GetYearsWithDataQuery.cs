using MediatR;

namespace BerrySystem.Core.Queries;

public class GetYearsWithDataQuery : IRequest<List<int>>
{
    public Guid BerryTypeId { get; set; }
}