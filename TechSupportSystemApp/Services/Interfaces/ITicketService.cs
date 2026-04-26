using TechSupportSystemApp.DTOs;

namespace TechSupportSystemApp.Services.Interfaces;

public interface ITicketService
{
    Task<List<TicketResponseDTO>> GetAllTicketsAsync();
    Task<TicketResponseDTO?> GetTicketByIdAsync(int id);
    Task<TicketResponseDTO> CreateTicketAsync(NewTicketDTO dto);
    Task DeleteTicketAsync(int id);
}