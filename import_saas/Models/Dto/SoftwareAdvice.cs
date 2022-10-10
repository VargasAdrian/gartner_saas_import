using System.ComponentModel.DataAnnotations;

namespace import_saas.Models.Dto;

public class SoftwareAdvice
{
    public SoftwareAdviceProduct[] products { get; set; }
}

public class SoftwareAdviceProduct
{
    public string title { get; set; }
    public string? twitter { get; set; }
    public string[] categories { get; set; }
}