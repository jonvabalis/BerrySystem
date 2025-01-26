using BerrySystem.Domain.Types;
using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateSaleCommand  : IRequest<Guid>
{
    public double Kilograms { get; set; }
    public double PricePerKilo { get; set; }
    public double TotalPrice { get; set; }
    public Guid EmployeeId { get; set; }
    public SaleType SaleType { get; set; }
    public DateTime? EventTime { get; set; }
    public Guid BerryTypeId { get; set; }
    public Guid? BerryKindId { get; set; }
}