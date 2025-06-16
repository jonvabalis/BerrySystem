using BerrySystem.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class StatisticsController : BaseController
{
    [HttpGet("GetCollectionStatisticsAllTime")]
    public async Task<IActionResult> GetCollectionStatisticsAllTime([FromQuery] GetCollectionStatisticsAllTimeQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetCollectionStatisticsFiltered")]
    public async Task<IActionResult> GetCollectionStatisticsFiltered([FromQuery] GetCollectionStatisticsFilteredQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetCostsStatisticsAllTime")]
    public async Task<IActionResult> GetCostsStatisticsAllTime([FromQuery] GetCostsStatisticsAllTimeQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetCostsStatisticsFiltered")]
    public async Task<IActionResult> GetCostsStatisticsFiltered([FromQuery] GetCostsStatisticsFilteredQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("GetCompareByYearStatistics")]
    public async Task<IActionResult> GetCompareByYearStatistics([FromQuery] GetCompareByYearStatisticsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("GetYearsWithData")]
    public async Task<IActionResult> GetYearsWithData([FromQuery] GetYearsWithDataQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}