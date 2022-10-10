using AutoMapper;
using import_saas.Importers;
using import_saas.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace import_saas;

public class App
{
    private readonly IMapper _mapper;
    private readonly IDbService _dbService;
    private readonly IConfiguration _config;
    private readonly ILogger<App> _logger;

    public App(IMapper mapper, IDbService dbService, IConfiguration config, ILogger<App> logger)
    {
        _mapper = mapper;
        _dbService = dbService;
        _config = config;
        _logger = logger;
    }

    public void Run(string[] args)
    {
        var importers = new List<IImporter>()
        {
            new CapterraImporter(_mapper, _dbService, _config),
            new SoftwareAdviceImporter(_mapper, _dbService, _config),
        };

        foreach (var importer in importers)
        {
            try 
            {
                _logger.LogInformation($"Starting importer: {importer.Name}");

                importer.Execute();

                _logger.LogInformation($"Completed importer: {importer.Name}");
            }
            catch(FileNotFoundException e)
            {
                _logger.LogError($"File not found for importer: {importer.Name}");
            }
            catch(Exception e)
            {
                _logger.LogError($"Unexpected error for importer: {importer.Name} - {e.Message}");
            }
        }
    }
}