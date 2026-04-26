using TechSupportSystemApp.Models;
namespace TechSupportSystemApp.DTOs;


public class TicketResponseDTO
{
    public int TicketId { get; set; }
    public string TicketTitle { get; set; } = string.Empty;
    public string TicketDescription { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public TicketStatus Status { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public List<string> Categories { get; set; } = new();
}