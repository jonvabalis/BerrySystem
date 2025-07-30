using BerrySystem.Core.Commands;
using BerrySystem.Core.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class CostController : BaseController
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateCostCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCostsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetCostsSum")]
    public async Task<IActionResult> GetAll([FromQuery] GetCostsSumQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}