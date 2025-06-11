using MediatR;

namespace BerrySystem.Core.Commands;

public class DeleteSaleCommand : IRequest<bool>
{
    public Guid SaleId { get; set; }
}