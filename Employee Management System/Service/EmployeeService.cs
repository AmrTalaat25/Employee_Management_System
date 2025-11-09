using Employee_Management_System.DTO;
using Employee_Management_System.Models;
using Employee_Management_System.Moduls.Data;
using Employee_Management_System.Service.Apstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Employee_Management_System.Service;

public class EmployeeService : IemployeeService
{
    private readonly ApplicationDbContext _context;
    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;

    }
    public enum Validationflag
    {
        create,
        update
    }

    #region GetAll
    public async Task<List<EmployeeDto>> GetAllEmployees(CancellationToken cancellationToken)
    {
        try
        {
            var employees = _context.Employees
                .AsNoTracking()
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.Id,
                    EmployeeName = e.FullName,
                    EmployeeEmail = e.Email,
                    EmployeePosition = e.Position,
                    EmployeeDepartment = e.Department,
                    EmployeeSalary = e.Salary,
                    EmployeeHireDate = e.HireDate
                }).ToList();
            Task.WaitAll();

            if (employees.Count == 0)
                throw new BadHttpRequestException("Not found Employee in database");

            return employees;
        }
        catch (Exception ex)
        {
            throw new BadHttpRequestException("can't get data of all employees");
        }
    }
    #endregion

    #region GET BY ID
    public EmployeeDto GetEmployeesById(int id)
    {
        var emp = _context.Employees.Find(id);
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
    #endregion

    #region Create Employee
    public async Task<EmployeeDto> CreateEmployee(CreateEmployeeDto createEmployeeDto)
    {
        try
        {
           
            await EmployeeValidator.ValidateAsync(createEmployeeDto, _context,Validationflag.create);

            var emp = new Employee
            {
                FullName = createEmployeeDto.EmployeeName,
                Email = createEmployeeDto.EmployeeEmail,
                Position = createEmployeeDto.EmployeePosition,
                Department = createEmployeeDto.EmployeeDepartment,
                Salary = createEmployeeDto.EmployeeSalary.Value,
                HireDate = createEmployeeDto.EmployeeHireDate
            };

            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();

            return new EmployeeDto
            {
                EmployeeId = emp.Id,
                EmployeeName = emp.FullName,
                EmployeeEmail = emp.Email,
                EmployeePosition = emp.Position,
                EmployeeDepartment = emp.Department,
                EmployeeSalary = emp.Salary,
                EmployeeHireDate = emp.HireDate
            };
        }
        catch (ArgumentException ex)
        {
            // validation error
            throw new BadHttpRequestException($"Validation failed: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            // duplicate email or logical error
            throw new BadHttpRequestException(ex.Message);
        }
        catch (Exception ex)
        {
            // unexpected errors
            throw new BadHttpRequestException($"Unexpected error while creating employee: {ex.Message}");
        }
    }

    #endregion

    #region Update Employee
    public async Task<string> UpdateEmployees(UpdateEmployeeDto Request)
    {
        try
        {
            await EmployeeValidator.ValidateAsync(Request, _context,Validationflag.update);

            bool emailExists = await _context.Employees.AnyAsync(e => e.Email == Request.EmployeeEmail && e.Id != Request.Id);
            if (emailExists is true)
                throw new InvalidOperationException("This email already exists.");

            var emp = _context.Employees.Find(Request.Id);
            if (emp is null)
                return $"ID of # {Request.Id} Not found";

            emp.FullName = Request.EmployeeName ?? emp.FullName;
            emp.Email = Request.EmployeeEmail ?? emp.Email;
            emp.Position = Request.EmployeePosition ?? emp.Position;
            emp.Department = Request.EmployeeDepartment ?? emp.Department;
            emp.Salary = Request.EmployeeSalary!.Value;
            emp.HireDate = Request.EmployeeHireDate;
            await _context.SaveChangesAsync();

            return $"thanks , {emp.FullName}  your data Is Updated successfuly";
        }
        catch (ArgumentException ex)
        {
            // validation error
            throw new BadHttpRequestException($"Validation failed: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            // duplicate email or logical error
            throw new BadHttpRequestException(ex.Message);
        }
        catch (Exception ex)
        {
            // unexpected errors
            throw new BadHttpRequestException($"Unexpected error while creating employee: {ex.Message}");
        }
    }
    #endregion

    #region Delete Employee
    public async Task<string> DeleteEmployee(int id)
    {
        var emp = await _context.Employees.FirstOrDefaultAsync(a=>a.Id == id);
        if (emp is null)
            return "this employee Not found";

        _context.Remove(emp);
        _context.SaveChanges();
        return $" employee of # {id} is Removed .";


    }
    #endregion




}
