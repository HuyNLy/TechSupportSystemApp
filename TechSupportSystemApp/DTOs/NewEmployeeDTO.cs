using System.ComponentModel.DataAnnotations;

namespace TechSupportSystemApp.DTOs;

public class NewEmployeeDTO
{
    [Required]
    public string EName { get; set; } = string.Empty;
}