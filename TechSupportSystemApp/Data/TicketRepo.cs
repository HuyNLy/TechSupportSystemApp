using Microsoft.EntityFrameworkCore;
using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Data;

public class TicketRepo : ITicketRepo
{
    private readonly AppDbContext _context;

    public TicketRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        return await _context.Tickets
            .Include(t => t.Employee)
            .Include(t => t.Categories)
            .ToListAsync();
    }

    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.Employee)
            .Include(t => t.Categories)
            .FirstOrDefaultAsync(t => t.ticketId == id);
    }

    public async Task<Ticket> CreateTicketAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task DeleteTicketAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Category>> GetCategoriesByIdsAsync(List<int> ids)
    {
        return await _context.Categories
            .Where(c => ids.Contains(c.catId))
            .ToListAsync();
    }
}
    