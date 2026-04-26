namespace TechSupportSystemApp.DTOs;

public class EmployeeResponseDTO
{
    public int EId { get; set; }
    public string EName { get; set; } = string.Empty;
    public List<string> Tickets { get; set; } = new();
}