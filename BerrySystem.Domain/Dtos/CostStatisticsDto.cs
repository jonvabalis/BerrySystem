using BerrySystem.Domain.Utilities;

namespace BerrySystem.Domain.Dtos;

public class CostStatisticsDto
{
    public Dictionary<int, CostsSum> Data { get; set; }
    public double Sum { get; set; }

    public CostStatisticsDto()
    {
        Data = new Dictionary<int, CostsSum>();
        Sum = 0;
    }
}