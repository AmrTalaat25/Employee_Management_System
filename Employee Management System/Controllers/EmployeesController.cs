using Employee_Management_System.DTO;
using Employee_Management_System.Moduls.Data;
using Employee_Management_System.Service.Apstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Employee_Management_System.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{

    private readonly IemployeeService _service;
    public EmployeesController(IemployeeService servece )
    {
        _service = servece;
    }


    [HttpGet ("GetEmployees")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)   
    {
        var employees = _service.GetAllEmployees(cancellationToken);
        return Ok(employees);
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var employees = _service.GetEmployeesById(id);  // TODO try , catch  // async, await
        if (employees is null )
        {
            return NotFound($"Not found Employee of # {id} in database");
        }
        return Ok(employees);
    }

    [HttpPost("Create Employee")]
    public Task<EmployeeDto> Create(CreateEmployeeDto createEmployeeDto)
    {
        return _service.CreateEmployee(createEmployeeDto);

    }

    [HttpPut("UpdateEmployees")]
    public async Task<string> UpdateEmployees(UpdateEmployeeDto updateEmployeeDto)
    {
        return await _service.UpdateEmployees(updateEmployeeDto);
    }
    [HttpDelete]
    public async Task<string> DeleteEmployee(int id)
    {
        return await _service.DeleteEmployee(id);
    }





}
