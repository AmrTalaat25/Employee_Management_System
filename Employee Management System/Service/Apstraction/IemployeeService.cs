using Employee_Management_System.DTO;

namespace Employee_Management_System.Service.Apstraction;

public interface IemployeeService
{
    Task<List<EmployeeDto>> GetAllEmployees(CancellationToken cancellationToken);
    EmployeeDto GetEmployeesById(int id);
    Task<EmployeeDto> CreateEmployee(CreateEmployeeDto createEmployeeDto);
    Task<string> UpdateEmployees(UpdateEmployeeDto updateEmployeeDto);
    Task<string> DeleteEmployee(int id);




}
