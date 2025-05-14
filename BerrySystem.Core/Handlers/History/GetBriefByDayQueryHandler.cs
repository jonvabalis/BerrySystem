using BerrySystem.Core.Queries;
using BerrySystem.Domain.Dtos;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.History;

public class GetBriefByDayQueryHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<GetBriefByDayQuery, BriefStatisticDto>
{
    public async Task<BriefStatisticDto> Handle(GetBriefByDayQuery request, CancellationToken cancellationToken)
    {
        var employees = new Dictionary<Guid, EmployeeBriefDto>();
        double totalHarvests = 0;
        double totalSold = 0;
        double totalSum = 0;
        
        await foreach (var harvest in berrySystemDbContext.Harvests
                           .Where(harvest => DateOnly.FromDateTime(harvest.EventTime) == request.Date)
                           .Include(harvest => harvest.Employee)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            if (!employees.ContainsKey(harvest.EmployeeId))
            {
                employees.TryAdd(harvest.EmployeeId, new EmployeeBriefDto
                {
                    Name = $"{harvest.Employee.FirstName} {harvest.Employee.LastName}",
                    HarvestedCount = 0,
                    SoldCount = 0
                });
            }
            
            employees[harvest.EmployeeId].HarvestedCount += harvest.Kilograms;
            totalHarvests += harvest.Kilograms;
        }
        
        await foreach (var sale in berrySystemDbContext.Sales
                           .Where(sale => DateOnly.FromDateTime(sale.EventTime) == request.Date)
                           .Include(harvest => harvest.Employee)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            if (!employees.ContainsKey(sale.EmployeeId))
            {
                employees.TryAdd(sale.EmployeeId, new EmployeeBriefDto
                {
                    Name = $"{sale.Employee.FirstName} {sale.Employee.LastName}",
                    HarvestedCount = 0,
                    SoldCount = 0
                });
            }
            
            employees[sale.EmployeeId].SoldCount += sale.Kilograms;
            totalSold += sale.Kilograms;
            totalSum += sale.TotalPrice;
        }

        return new BriefStatisticDto
        {
            Employees = employees,
            HarvestedCount = totalHarvests,
            SoldCount = totalSold,
            SoldSum = totalSum
        };
    }
}