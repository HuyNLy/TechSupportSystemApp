using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDTO>> GetAllAsync();
    Task<CategoryResponseDTO?> GetByIdAsync(int id);
    Task<CategoryResponseDTO> CreateAsync(Category category);
    Task<bool> DeleteAsync(int id);
}