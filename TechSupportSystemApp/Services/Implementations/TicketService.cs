using TechSupportSystemApp.Data;
using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Models;
using TechSupportSystemApp.Services.Interfaces;

namespace TechSupportSystemApp.Services.Implementations;

public class TicketService : ITicketService
{
    private readonly ITicketRepo _repo;

    public TicketService(ITicketRepo repo)
    {
        _repo = repo;
    }

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        return await _repo.GetAllTicketsAsync();
    }

    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        return await _repo.GetTicketByIdAsync(id);
    }

    public async Task<Ticket> CreateTicketAsync(NewTicketDTO dto)
    {
        // Map DTO → Model (trainer style)
        var ticket = new Ticket
        {
            ticketTitle = dto.Title,
            ticketDescription = dto.Description,
            EmployeeId = dto.EmployeeId
        };

        // Attach categories
        ticket.Categories = await _repo.GetCategoriesByIdsAsync(dto.CategoryIds);

        // Save
        return await _repo.CreateTicketAsync(ticket);
    }

    public async Task DeleteTicketAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException("ID must be greater than 0!");

        var ticket = await _repo.GetTicketByIdAsync(id);

        if (ticket is null)
            throw new KeyNotFoundException("This ticket doesn't exist.");

        await _repo.DeleteTicketAsync(ticket);
    }
}
