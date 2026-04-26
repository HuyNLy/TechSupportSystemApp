using TechSupportSystemApp.Data;
using TechSupportSystemApp.Models;
using TechSupportSystemApp.Services.Interfaces;
using TechSupportSystemApp.DTOs;

namespace TechSupportSystemApp.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepo _repo;

    public EmployeeService(IEmployeeRepo repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
        => await _repo.GetAllEmployeesAsync();

    public async Task<Employee?> GetByIdAsync(int id)
        => await _repo.GetEmployeeByIdAsync(id);

    public async Task<Employee> CreateAsync(NewEmployeeDTO dto)
    {
        var employee = new Employee
        {
            EName = dto.EName
        };
        return await _repo.CreateEmployeeAsync(employee);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _repo.GetEmployeeByIdAsync(id);
        if (employee is null) return false;

        await _repo.DeleteEmployeeAsync(employee);
        return true;
    }
}