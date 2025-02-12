using BerrySystem.Domain.Utilities;

namespace BerrySystem.Domain.Dtos;

public class CollectionStatisticsDto
{
    public Dictionary<int, CollectionStatisticsLine> Data { get; set; }
    public CollectionStatisticsLine Sum { get; set; }

    public CollectionStatisticsDto()
    {
        Data = new();
        Sum = new(0, 0, 0);
    }
}