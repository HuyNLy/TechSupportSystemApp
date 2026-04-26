using System.ComponentModel.DataAnnotations;

namespace TechSupportSystemApp.Models;

public class Ticket
{
    [Key]
    public int TicketId { get; set; }

    [Required]
    [MaxLength(200)]
    public string TicketTitle { get; set; } = string.Empty;

    [MaxLength(500)]
    public string TicketDescription { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public TicketStatus Status { get; set; } = TicketStatus.Open;

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public List<Category> Categories { get; set; } = new();
}

// Just add it here at the bottom of the same file
public enum TicketStatus
{
    Open,
    Closed
}