using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee> CreateAsync(Employee employee);
    Task<bool> DeleteAsync(int id);
}
