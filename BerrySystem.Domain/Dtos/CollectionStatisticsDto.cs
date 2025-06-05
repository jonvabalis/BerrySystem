using BerrySystem.Domain.Utilities;

namespace BerrySystem.Domain.Dtos;

public class CollectionStatisticsDto
{
    public Dictionary<int, CollectionStatisticsLine> Data { get; set; } = new();
    public CollectionStatisticsLine Sum { get; set; } = new(0, 0, 0);
}