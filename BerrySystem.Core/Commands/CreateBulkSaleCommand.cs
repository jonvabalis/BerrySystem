using BerrySystem.Domain.Utilities;
using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateBulkSaleCommand : IRequest<Unit>
{
    public required List<BulkSale> Sales { get; set; }
}