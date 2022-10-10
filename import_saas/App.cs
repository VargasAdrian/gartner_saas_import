using AutoMapper;
using import_saas.Importers;
using import_saas.Services;
using Microsoft.Extensions.Logging;

namespace import_saas;

public class App
{
    private readonly IMapper _mapper;
    private readonly IDbService _dbService;
    private readonly ILogger<App> _logger;

    public App(IMapper mapper, IDbService dbService, ILogger<App> logger)
    {
        _mapper = mapper;
        _dbService = dbService;
        _logger = logger;
    }

    public void Run(string[] args)
    {
        var importers = new List<IImporter>()
        {
            new CapterraImporter(_mapper, _dbService, _logger),
            new SoftwareAdviceImporter(_mapper, _dbService, _logger),
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
                
            }
            catch(Exception e)
            {

            }
        }
    }
}