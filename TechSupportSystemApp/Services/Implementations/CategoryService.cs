using TechSupportSystemApp.Data;
using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Models;
using TechSupportSystemApp.Services.Interfaces;

namespace TechSupportSystemApp.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepo _repo;

    public CategoryService(ICategoryRepo repo)
    {
        _repo = repo;
    }

    private static CategoryResponseDTO MapToDTO(Category c) => new()
    {
        CatId = c.CatId,
        CatName = c.CatName,
        Tickets = c.Tickets.Select(t => t.TicketTitle).ToList()
    };

    public async Task<IEnumerable<CategoryResponseDTO>> GetAllAsync()
        => (await _repo.GetAllCategoriesAsync()).Select(MapToDTO);

    public async Task<CategoryResponseDTO?> GetByIdAsync(int id)
    {
        var category = await _repo.GetCategoryByIdAsync(id);
        if (category is null) return null;
        return MapToDTO(category);
    }

    public async Task<CategoryResponseDTO> CreateAsync(Category category)
    {
        var created = await _repo.CreateCategoryAsync(category);
        return MapToDTO(created);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repo.GetCategoryByIdAsync(id);
        if (category is null) return false;
        await _repo.DeleteCategoryAsync(category);
        return true;
    }
}