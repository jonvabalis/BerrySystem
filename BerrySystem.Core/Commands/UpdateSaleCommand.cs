using BerrySystem.Domain.Types;
using MediatR;

namespace BerrySystem.Core.Commands;

public class UpdateSaleCommand : IRequest<bool>
{
    public Guid SaleId { get; set; }
    public Guid EmployeeId { get; set; }
    public double Kilograms { get; set; }
    public double PricePerKilo { get; set; }
    public double TotalPrice { get; set; }
    public SaleType SaleType { get; set; }
    public Guid? BerryKindId { get; set; }
}