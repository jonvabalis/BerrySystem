using BerrySystem.Core.Commands;
using BerrySystem.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class EmployeeController : BaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateEmployeeCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllEmployeesQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetById([FromQuery] GetByIdEmployeeQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}