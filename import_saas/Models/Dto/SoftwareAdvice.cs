using System.ComponentModel.DataAnnotations;

namespace import_saas.Models.Dto;

public class SoftwareAdvice
{
    [Required]
    public string title { get; set; }
    public string? twitter { get; set; }
    [Required]
    public IEnumerable<string> categories { get; set; }
}