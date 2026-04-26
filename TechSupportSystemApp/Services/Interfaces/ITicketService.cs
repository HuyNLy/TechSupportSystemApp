using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Services.Interfaces;

public interface ITicketService
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task<Ticket> CreateTicketAsync(NewTicketDTO dto);
    Task DeleteTicketAsync(int id);
}
