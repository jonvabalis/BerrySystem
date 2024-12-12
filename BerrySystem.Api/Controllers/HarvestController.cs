using BerrySystem.Core.Commands;
using BerrySystem.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class HarvestController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateHarvestCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllHarvestsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}