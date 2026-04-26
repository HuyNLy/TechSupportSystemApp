using System.ComponentModel.DataAnnotations;

namespace TechSupportSystemApp.Models;

public class Category
{
    [Key]
    public int CatId { get; set; }
    [Required]
    [MaxLength(100)]
    public string CatName { get; set; } = string.Empty;

    // M-M relationship: A category can belong to many tickets
    public List<Ticket> Tickets { get; set; } = new();
}
