using TechSupportSystemApp.Data;
using TechSupportSystemApp.Models;
using TechSupportSystemApp.Services.Interfaces;
using TechSupportSystemApp.DTOs;

namespace TechSupportSystemApp.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepo _repo;

    public CategoryService(ICategoryRepo repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await _repo.GetAllCategoriesAsync();

    public async Task<Category?> GetByIdAsync(int id)
        => await _repo.GetCategoryByIdAsync(id);

    public async Task<Category> CreateAsync(NewCategoryDTO dto)
    {
        var category = new Category
        {
            CatName = dto.CatName
        };
        return await _repo.CreateCategoryAsync(category);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repo.GetCategoryByIdAsync(id);
        if (category is null) return false;

        await _repo.DeleteCategoryAsync(category);
        return true;
    }
}