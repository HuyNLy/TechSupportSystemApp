using TechSupportSystemApp.Data;
using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Services.Interfaces;

namespace TechSupportSystemApp.Services.Implementations;

public class TicketService : ITicketService
{
    private readonly ITicketRepo _repo;

    public TicketService(ITicketRepo repo)
    {
        _repo = repo;
    }

    // Helper to avoid repeating the mapping in every method
    private static TicketResponseDTO MapToDTO(Models.Ticket t) => new()
    {
        TicketId = t.TicketId,
        TicketTitle = t.TicketTitle,
        TicketDescription = t.TicketDescription,
        CreatedAt = t.CreatedAt,
        EmployeeName = t.Employee.EName,
        Categories = t.Categories.Select(c => c.CatName).ToList()
    };

    public async Task<List<TicketResponseDTO>> GetAllTicketsAsync()
    {
        var tickets = await _repo.GetAllTicketsAsync();
        return tickets.Select(MapToDTO).ToList();
    }

    public async Task<TicketResponseDTO?> GetTicketByIdAsync(int id)
    {
        var ticket = await _repo.GetTicketByIdAsync(id);
        if (ticket is null) return null;
        return MapToDTO(ticket);
    }

    public async Task<TicketResponseDTO> CreateTicketAsync(NewTicketDTO dto)
    {
        var ticket = new Models.Ticket
        {
            TicketTitle = dto.Title!,
            TicketDescription = dto.Description ?? string.Empty,
            EmployeeId = dto.EmployeeId
        };

        ticket.Categories = await _repo.GetCategoriesByIdsAsync(dto.CategoryIds!);

        var created = await _repo.CreateTicketAsync(ticket);

        // Reload with includes so Employee + Categories are populated for mapping
        var full = await _repo.GetTicketByIdAsync(created.TicketId);
        return MapToDTO(full!);
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