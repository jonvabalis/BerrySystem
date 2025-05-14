namespace BerrySystem.Domain.Dtos;

public class HistoryBriefStatisticsDto
{
    public required Dictionary<Guid, HistoryBriefEmployeeDto> Employees { get; set; }
    public required HistoryBriefStatisticsTotalDto Totals { get; set; }
}