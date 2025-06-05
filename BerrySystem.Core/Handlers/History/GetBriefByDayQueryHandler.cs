using BerrySystem.Core.Queries;
using BerrySystem.Domain.Dtos;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.History;

public class GetBriefByDayQueryHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<GetBriefByDayQuery, HistoryBriefStatisticsDto>
{
    public async Task<HistoryBriefStatisticsDto> Handle(GetBriefByDayQuery request, CancellationToken cancellationToken)
    {
        var employees = new Dictionary<Guid, HistoryBriefEmployeeDto>();
        var totals = new HistoryBriefStatisticsTotalDto();

        await foreach (var harvest in berrySystemDbContext.Harvests
                           .Where(harvest => harvest.BerryType.Id == request.BerryTypeId && DateOnly.FromDateTime(harvest.EventTime) == request.Date)
                           .Include(harvest => harvest.Employee)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            if (!employees.ContainsKey(harvest.EmployeeId))
            {
                employees.TryAdd(harvest.EmployeeId, new HistoryBriefEmployeeDto
                {
                    Name = $"{harvest.Employee.FirstName} {harvest.Employee.LastName}",
                    HarvestedCount = 0,
                    SoldCount = 0
                });
            }

            employees[harvest.EmployeeId].HarvestedCount += harvest.Kilograms;
            totals.HarvestedCount += harvest.Kilograms;
        }

        await foreach (var sale in berrySystemDbContext.Sales
                           .Where(sale => sale.BerryType.Id == request.BerryTypeId && DateOnly.FromDateTime(sale.EventTime) == request.Date)
                           .Include(harvest => harvest.Employee)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            if (!employees.ContainsKey(sale.EmployeeId))
            {
                employees.TryAdd(sale.EmployeeId, new HistoryBriefEmployeeDto
                {
                    Name = $"{sale.Employee.FirstName} {sale.Employee.LastName}",
                    HarvestedCount = 0,
                    SoldCount = 0
                });
            }

            employees[sale.EmployeeId].SoldCount += sale.Kilograms;
            totals.SoldCount += sale.Kilograms;
            totals.SoldSum += sale.TotalPrice;
        }

        return new HistoryBriefStatisticsDto
        {
            Employees = employees,
            Totals = totals
        };
    }
}