using Employee_Management_System.DTO;

namespace Employee_Management_System.Service.Apstraction;

public interface IemployeeService
{
    List<EmployeeDto> GetAllEmployees();
    EmployeeDto GetEmployeesById(int id);
    EmployeeDto CreateEmployee(CreateEmployeeDto createEmployeeDto);



}
