using BerrySystem.Core.Commands;
using BerrySystem.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class SaleController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateSaleCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllSalesQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("CreateBulk")]
    public async Task<IActionResult> CreateBulk(CreateBulkSaleCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPut("Update")]
    public async Task<IActionResult> Update(UpdateSaleCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("Delete/{saleId:guid}")]
    public async Task<IActionResult> Delete(Guid saleId)
    {
        var command = new DeleteSaleCommand { SaleId = saleId };
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet("GetByDate")]
    public async Task<IActionResult> GetByDate([FromQuery] GetSalesByDateQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}