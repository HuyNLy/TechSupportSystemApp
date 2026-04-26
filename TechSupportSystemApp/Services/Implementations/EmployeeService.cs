using TechSupportSystemApp.Data;
using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Models;
using TechSupportSystemApp.Services.Interfaces;

namespace TechSupportSystemApp.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepo _repo;

    public EmployeeService(IEmployeeRepo repo)
    {
        _repo = repo;
    }

    private static EmployeeResponseDTO MapToDTO(Employee e) => new()
    {
        EId = e.EId,
        EName = e.EName,
        Tickets = e.Tickets.Select(t => t.TicketTitle).ToList()
    };

    public async Task<IEnumerable<EmployeeResponseDTO>> GetAllAsync()
        => (await _repo.GetAllEmployeesAsync()).Select(MapToDTO);

    public async Task<EmployeeResponseDTO?> GetByIdAsync(int id)
    {
        var employee = await _repo.GetEmployeeByIdAsync(id);
        if (employee is null) return null;
        return MapToDTO(employee);
    }

    public async Task<EmployeeResponseDTO> CreateAsync(NewEmployeeDTO dto)
    {
        var employee = new Employee { EName = dto.EName };
        var created = await _repo.CreateEmployeeAsync(employee);
        return MapToDTO(created);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _repo.GetEmployeeByIdAsync(id);
        if (employee is null) return false;
        await _repo.DeleteEmployeeAsync(employee);
        return true;
    }
}