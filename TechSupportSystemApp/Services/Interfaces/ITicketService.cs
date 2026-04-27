using TechSupportSystemApp.DTOs;
using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Services.Interfaces;

public interface ITicketService
{
    Task<List<TicketResponseDTO>> GetAllTicketsAsync();
    Task<List<TicketResponseDTO>> GetTicketsByStatusAsync(TicketStatus status);
    Task<List<TicketResponseDTO>> GetTicketsByPriorityAsync(TicketPriority priority);
    Task<TicketResponseDTO?> GetTicketByIdAsync(int id);
    Task<TicketResponseDTO> CreateTicketAsync(NewTicketDTO dto);
    Task UpdateTicketAsync(int id, UpdateTicketDTO dto);
    Task DeleteTicketAsync(int id);
}