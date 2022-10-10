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

    public CapterraImporter(
        IMapper mapper,
        IDbService dbService
    )
    {
        _mapper = mapper;
        _dbService = dbService;
    }

    public string Name => "Capterra";

    public void Execute()
    {
        var yaml = File.ReadAllText("capterra.yaml");
        
        var deserializer = new YamlDotNet.Serialization.Deserializer();
        var capterraProducts = deserializer.Deserialize<List<Capterra>>(yaml);

        if(capterraProducts is null || capterraProducts.Count == 0)
        {
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
    }
}