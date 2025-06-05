using BerrySystem.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class HistoryController : BaseController
{
    [HttpGet("GetAllRecordedDaysByYear")]
    public async Task<IActionResult> GetAllRecordedDaysByYear([FromQuery] GetAllRecordedDaysByYearQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetBriefByDay")]
    public async Task<IActionResult> GetBriefByDay([FromQuery] GetBriefByDayQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}