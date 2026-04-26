using TechSupportSystemApp.DTOs;

namespace TechSupportSystemApp.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeResponseDTO>> GetAllAsync();
    Task<EmployeeResponseDTO?> GetByIdAsync(int id);
    Task<EmployeeResponseDTO> CreateAsync(NewEmployeeDTO dto);
    Task<bool> DeleteAsync(int id);
}