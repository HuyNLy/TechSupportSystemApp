using TechSupportSystemApp.Models;
using TechSupportSystemApp.DTOs;

namespace TechSupportSystemApp.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category> CreateAsync(NewCategoryDTO dto);
    Task<bool> DeleteAsync(int id);
}
