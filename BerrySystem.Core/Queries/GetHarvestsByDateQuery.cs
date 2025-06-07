using BerrySystem.Domain.Utilities;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetHarvestsByDateQuery : IRequest<List<HarvestDataLine>>
{
    public Guid BerryTypeId { get; set; }
    public DateTime HarvestDate { get; set; }
}