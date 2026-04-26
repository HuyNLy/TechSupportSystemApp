using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Data;

public interface ICategoryRepo
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category> CreateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
}