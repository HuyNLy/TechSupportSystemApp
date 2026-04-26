namespace TechSupportSystemApp.DTOs;

public class CategoryResponseDTO
{
    public int CatId { get; set; }
    public string CatName { get; set; } = string.Empty;
    public List<string> Tickets { get; set; } = new();
}