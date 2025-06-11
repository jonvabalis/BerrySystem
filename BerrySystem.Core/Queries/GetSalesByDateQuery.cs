using BerrySystem.Domain.Utilities;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetSalesByDateQuery : IRequest<List<SaleDataLine>>
{
    public Guid BerryTypeId { get; set; }
    public DateTime SaleDate { get; set; }
}