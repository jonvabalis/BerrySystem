using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateBulkSaleCommand : IRequest<Unit>
{
    public required List<CreateSaleCommand> Sales { get; set; }
}