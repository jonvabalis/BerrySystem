using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateCostCommand : IRequest<Guid>
{
    public double Price { get; set; }
}