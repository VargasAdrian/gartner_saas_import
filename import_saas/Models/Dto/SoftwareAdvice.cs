namespace import_saas.Models.Dto;

public class SoftwareAdvice
{
    public string title { get; set; }
    public string? twitter { get; set; }
    public IEnumerable<string> categories { get; set; }
}