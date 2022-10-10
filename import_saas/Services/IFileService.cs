using Microsoft.Extensions.Configuration;

namespace import_saas.Services;

public interface IFileService
{
    string SoftwareAdvice { get; }
    string Capterra { get; }
}

public class FileService : IFileService
{
    private readonly string _SoftwareAdvice;
    public string SoftwareAdvice => _SoftwareAdvice;

    private readonly string _Capterra;
    public string Capterra => _Capterra;

    public FileService(IConfiguration config)
    {
        var sources = "FileSources";
        
        _SoftwareAdvice = config.GetSection(sources)["SoftwareAdvice"];
        _Capterra = config.GetSection(sources)["Capterra"];
    }
}