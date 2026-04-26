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

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Required because FK is non-nullable
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    // M-M relationship 
    public List<Category> Categories { get; set; } = new();
}
