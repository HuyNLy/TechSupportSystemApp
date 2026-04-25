using System.ComponentModel.DataAnnotations;

namespace TechSupportSystemApp.Models;

public class Ticket
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Required because FK is non-nullable
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    // M-M relationship (never mark as [Required])
    public List<Category> Categories { get; set; } = new();
}
