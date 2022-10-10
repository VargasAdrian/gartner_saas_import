using System.ComponentModel.DataAnnotations;

namespace import_saas.Models.Dto;

public class Capterra
{
    [Required]
    public string name { get; set; }
    public string? twitter { get; set; }
    [Required]
    public string tags { get; set; }
}