using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Data;

public interface IEmployeeRepo
{
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(int id);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(Employee employee);
}