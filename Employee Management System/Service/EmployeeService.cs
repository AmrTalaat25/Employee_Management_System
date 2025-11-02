using Employee_Management_System.DTO;
using Employee_Management_System.Moduls.Data;
using Employee_Management_System.Service.Apstraction;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Service;

public class EmployeeService : IemployeeService
{
    private readonly ApplicationDbContext _context;
    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;

    }

    public List<EmployeeDto> GetAllEmployees()
    {
        var employees = _context.Employees.Select(e => new EmployeeDto
        {
            EmployeeId = e.Id,
            EmployeeName = e.FullName,
            EmployeeEmail = e.Email,
            EmployeePosition = e.Position,
            EmployeeDepartment = e.Department,
            EmployeeSalary = e.Salary,
            EmployeeHireDate = e.HireDate
        }).ToList();
        return employees;
    }
}
