using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.DTOs;

public class UpdateTicketDTO
{
    public string? TicketTitle { get; set; }
    public string? TicketDescription { get; set; }
    public TicketPriority? Priority { get; set; }
    public TicketStatus? Status { get; set; }
}