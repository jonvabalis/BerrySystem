using BerrySystem.Domain.Utilities;

namespace BerrySystem.Domain.Dtos;

public class CostStatisticsDto
{
    public Dictionary<int, CostsSum> Data { get; set; }
    public CostsSum Sum { get; set; }

    public CostStatisticsDto()
    {
        Data = new Dictionary<int, CostsSum>();
        Sum = new CostsSum();
    }
}