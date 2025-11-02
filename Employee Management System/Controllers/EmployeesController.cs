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
    private readonly ApplicationDbContext _context;
    private readonly IemployeeService _servece;
    public EmployeesController(ApplicationDbContext context, IemployeeService servece )
    {
        _context = context;
        _servece = servece;
    }
    // TO-DO service to get all employess
    // TO-DO dto

    [HttpGet]
    public IActionResult GetAll()  
    {
        var employees = _servece.GetAllEmployees();
                 if (employees is null || !employees.Any())
        {
            return NotFound("No Employees found in the database");
        }
        return Ok(employees);
    }
}
