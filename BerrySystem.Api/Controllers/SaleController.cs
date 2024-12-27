using BerrySystem.Core.Commands;
using BerrySystem.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class SaleController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateSaleCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllSalesQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}