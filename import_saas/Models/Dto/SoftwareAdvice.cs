using System.ComponentModel.DataAnnotations;

namespace import_saas.Models.Dto;

public class SoftwareAdvice
{
    public List<SoftwareAdviceProduct> products { get; set; }
}

public class SoftwareAdviceProduct
{
    public string title { get; set; }
    public string? twitter { get; set; }
    public List<string> categories { get; set; }
}