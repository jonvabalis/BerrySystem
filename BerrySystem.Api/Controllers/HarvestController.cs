using BerrySystem.Core.Commands;
using BerrySystem.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class HarvestController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateHarvestCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllHarvestsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("CreateBulk")]
    public async Task<IActionResult> CreateBulk(CreateBulkHarvestCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPut("Update")]
    public async Task<IActionResult> Update(UpdateHarvestCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("Delete/{harvestId:guid}")]
    public async Task<IActionResult> Delete(Guid harvestId)
    {
        var command = new DeleteHarvestCommand { HarvestId = harvestId };
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet("GetByDate")]
    public async Task<IActionResult> GetByDate([FromQuery] GetHarvestsByDateQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}