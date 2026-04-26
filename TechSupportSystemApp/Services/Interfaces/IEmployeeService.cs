using TechSupportSystemApp.Models;
using TechSupportSystemApp.DTOs;

namespace TechSupportSystemApp.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee> CreateAsync(NewEmployeeDTO dto);
    Task<bool> DeleteAsync(int id);
}
