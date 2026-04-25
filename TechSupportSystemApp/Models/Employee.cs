using System.ComponentModel.DataAnnotations;

namespace TechSupportSystemApp.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;


    // 1-M relationship: One employee can submit many tickets
    public List<Ticket> Tickets { get; set; } = new();
}
