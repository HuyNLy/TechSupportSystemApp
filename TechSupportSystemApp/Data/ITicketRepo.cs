using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Data;

public interface ITicketRepo
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task<Ticket> CreateTicketAsync(Ticket ticket);
    Task DeleteTicketAsync(Ticket ticket);

    // Needed for attaching categories when creating a ticket
    Task<List<Category>> GetCategoriesByIdsAsync(List<int> ids);
}
