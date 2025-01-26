using BerrySystem.Core.Commands;
using BerrySystem.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class BerryTypeController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateBerryTypeCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBerryTypesQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetById([FromQuery] GetByIdBerryTypeQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}