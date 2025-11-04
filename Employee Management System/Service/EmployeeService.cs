using Employee_Management_System.DTO;
using Employee_Management_System.Models;
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
    public EmployeeDto GetEmployeesById(int id)
    {
        var emp= _context.Employees.Find(id);
        if (emp is null)
            return null!;
        var employeeDto = new EmployeeDto
        {
            EmployeeId = emp.Id,
            EmployeeName = emp.FullName,
            EmployeeEmail = emp.Email,
            EmployeePosition = emp.Position,
            EmployeeDepartment = emp.Department,
            EmployeeSalary = emp.Salary,
            EmployeeHireDate = emp.HireDate

        };
        return employeeDto;
    }
    public EmployeeDto CreateEmployee(CreateEmployeeDto createEmployeeDto)
    {
        var emp = new Employee
        {
            FullName = createEmployeeDto.EmployeeName,
            Email = createEmployeeDto.EmployeeEmail,
            Position = createEmployeeDto.EmployeePosition,
            Department = createEmployeeDto.EmployeeDepartment,
            Salary = createEmployeeDto.EmployeeSalary,
            HireDate = createEmployeeDto.EmployeeHireDate

        };
        _context.Employees.Add(emp);
        _context.SaveChanges();
        var employeeDto = new EmployeeDto
        {
            EmployeeId = emp.Id,
            EmployeeName = emp.FullName,
            EmployeeEmail = emp.Email,
            EmployeePosition = emp.Position,
            EmployeeDepartment = emp.Department,
            EmployeeSalary = emp.Salary,
            EmployeeHireDate = emp.HireDate

        };
        return employeeDto;
    }

}
