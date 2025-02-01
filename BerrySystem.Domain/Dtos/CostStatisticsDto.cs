namespace BerrySystem.Domain.Dtos;

public class CostStatisticsDto
{
    public Dictionary<int, double> Data { get; set; }
    public double Sum { get; set; }
    
    public CostStatisticsDto()
    {
        Data = new();
        Sum  = 0;
    }
}