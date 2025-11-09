
using Employee_Management_System.DTO;
using Employee_Management_System.Moduls.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static Employee_Management_System.Service.EmployeeService;

public static class EmployeeValidator
{
    public static async Task ValidateAsync(CreateEmployeeDto dto, ApplicationDbContext context , Validationflag? ctorName)
    {
        switch(ctorName)
        {
            case Validationflag.create:

                // Name validation 
                if (  string.IsNullOrWhiteSpace(dto.EmployeeName) || !Regex.IsMatch(dto.EmployeeName, @"^[a-zA-Z\s]+$"))
                    throw new ArgumentException("Employee name is required and must contain only letters and spaces.");
                if (dto.EmployeeName.Length < 3 || dto.EmployeeName.Length > 100)
                    throw new ArgumentException("Name must be between 3 and 100 characters.");

                // Email validation
                bool emailExists = await context.Employees.AnyAsync(e => e.Email == dto.EmployeeEmail);
                if (emailExists is true)
                    throw new InvalidOperationException("This email already exists.");
                if (string.IsNullOrWhiteSpace(dto.EmployeeEmail) ||
                    !Regex.IsMatch(dto.EmployeeEmail, @"^[a-zA-Z0-9.]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                    throw new ArgumentException("Invalid email format.");

                // Position validation
                if (string.IsNullOrWhiteSpace(dto.EmployeePosition) || !Regex.IsMatch(dto.EmployeePosition, @"^[a-zA-Z\s]+$"))
                    throw new ArgumentException("Employee position is required and must contain only letters and spaces.");

                // Department validation
                if (string.IsNullOrWhiteSpace(dto.EmployeeDepartment) || !Regex.IsMatch(dto.EmployeeDepartment, @"^[a-zA-Z-_.\s]+$"))
                    throw new ArgumentException("Employee department is required and must contain only letters, spaces, or -._");

                // Salary validation
                if (dto.EmployeeSalary < 1000 || dto.EmployeeSalary > 50000)
                    throw new ArgumentException("Salary must be between 1000 and 50000.");

                break;

                case Validationflag.update:
                // Name validation 
                if(dto.EmployeeName!=null && !Regex.IsMatch(dto.EmployeeName, @"^[a-zA-Z\s]+$"))
                    throw new ArgumentException("Employee name is required and must contain only letters and spaces.");
                if (dto.EmployeeName?.Length < 3 || dto.EmployeeName?.Length > 100)
                    throw new ArgumentException("Name must be between 3 and 100 characters.");

                // Email validation
                if (dto.EmployeeEmail != null &&
                    !Regex.IsMatch(dto.EmployeeEmail, @"^[a-zA-Z0-9.]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                    throw new ArgumentException("Invalid email format.");

                // Position validation
                if (dto.EmployeePosition !=null && (!Regex.IsMatch(dto.EmployeePosition, @"^[a-zA-Z\s]+$")))
                    throw new ArgumentException("Employee position is required and must contain only letters and spaces.");

                // Department validation
                if (dto.EmployeeDepartment !=null && (!Regex.IsMatch(dto.EmployeeDepartment, @"^[a-zA-Z-_.\s]+$")))
                    throw new ArgumentException("Employee department is required and must contain only letters, spaces, or -._");

                // Salary validation
                if (dto.EmployeeSalary != null && (dto.EmployeeSalary < 1000 || dto.EmployeeSalary > 50000))
                    throw new ArgumentException("Salary must be between 1000 and 50000.");

                break;

        }

     
    }
}
