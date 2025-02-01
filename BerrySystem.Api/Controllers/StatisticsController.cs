using BerrySystem.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class StatisticsController : BaseController
{
    [HttpGet("GetCollectionStatisticsAllTime")]
    public async Task<IActionResult> Create([FromQuery] GetCollectionStatisticsAllTimeQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetCollectionStatisticsFiltered")]
    public async Task<IActionResult> GetAll([FromQuery] GetCollectionStatisticsFilteredQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("GetCostsStatisticsAllTime")]
    public async Task<IActionResult> Create([FromQuery] GetCostsStatisticsAllTimeQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetCostsStatisticsFiltered")]
    public async Task<IActionResult> GetAll([FromQuery] GetCostsStatisticsFilteredQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}