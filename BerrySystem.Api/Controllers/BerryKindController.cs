using BerrySystem.Core.Commands;
using BerrySystem.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class BerryKindController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateBerryKindCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { Id = result }, result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBerryKindsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetById([FromQuery] GetByIdBerryKindQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}