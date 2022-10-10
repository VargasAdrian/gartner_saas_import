using AutoMapper;
using import_saas.Models.Db.MySql;
using import_saas.Models.Dto;
using import_saas.Services;
using Microsoft.Extensions.Logging;

namespace import_saas.Importers;

public class CapterraImporter : IImporter
{
    private readonly IMapper _mapper;
    private readonly IDbService _dbService;
    private readonly ILogger<SoftwareAdviceImporter> _logger;

    public CapterraImporter(
        IMapper mapper,
        IDbService dbService,
        ILogger<SoftwareAdviceImporter> logger
    )
    {
        _mapper = mapper;
        _dbService = dbService;
        _logger = logger;
    }

    public string Name => "Capterra";

    public void Execute()
    {
        _logger.LogInformation("Starting Capterra import");

        var yaml = File.ReadAllText("capterra.yaml");
        
        var deserializer = new YamlDotNet.Serialization.Deserializer();
        var capterraProducts = deserializer.Deserialize<List<Capterra>>(yaml);

        if(capterraProducts is null || capterraProducts.Count == 0)
        {
            _logger.LogWarning("No products from Capterra to import");
            return;
        }

        var products = _mapper.Map<List<Capterra>, List<Product>>(capterraProducts);

        products.ForEach(p => 
        {
            if(_dbService.GetProduct(p.name) is null)
                _dbService.CreateProduct(p);
            else
                _dbService.UpdateProduct(p);
        });

        _logger.LogInformation("Finished importing Capterra");
    }
}