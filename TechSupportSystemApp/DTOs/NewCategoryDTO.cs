using System.ComponentModel.DataAnnotations;

namespace TechSupportSystemApp.DTOs;

public class NewCategoryDTO
{
    [Required]
    public string CatName { get; set; } = string.Empty;
}