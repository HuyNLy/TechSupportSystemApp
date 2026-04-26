using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Data;

public interface ITicketRepo
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<List<Ticket>> GetTicketsByStatusAsync(TicketStatus status);
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task<Ticket> CreateTicketAsync(Ticket ticket);
    Task UpdateTicketAsync();
    Task DeleteTicketAsync(Ticket ticket);
    Task<List<Category>> GetCategoriesByIdsAsync(List<int> ids);
}