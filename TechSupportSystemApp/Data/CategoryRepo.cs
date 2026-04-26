using Microsoft.EntityFrameworkCore;
using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Data;

public class CategoryRepo : ICategoryRepo
{
    private readonly AppDbContext _context;

    public CategoryRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
        => await _context.Categories
            .Include(c => c.Tickets)
            .ToListAsync();

    public async Task<Category?> GetCategoryByIdAsync(int id)
        => await _context.Categories
            .Include(c => c.Tickets)
            .FirstOrDefaultAsync(c => c.CatId == id);

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}