using BerrySystem.Core.Commands;
using BerrySystem.Core.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BerrySystem.Api.Controllers;

public class EmployeeController : BaseController
{
    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginEmployeeCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("Activate/{employeeId:guid}")]
    public async Task<IActionResult> Activate(Guid employeeId)
    {
        var command = new ActivateEmployeeCommand { EmployeeId = employeeId };
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetAllActive")]
    public async Task<IActionResult> GetAllActive([FromQuery] GetAllActiveEmployeesQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}