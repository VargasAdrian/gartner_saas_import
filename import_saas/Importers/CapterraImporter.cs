using AutoMapper;
using import_saas.Models.Db.MySql;
using import_saas.Models.Dto;
using import_saas.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace import_saas.Importers;

public class CapterraImporter : IImporter
{
    private readonly IMapper _mapper;
    private readonly IDbService _dbService;
    private readonly IConfiguration _config;

    public CapterraImporter(
        IMapper mapper,
        IDbService dbService,
        IConfiguration config
    )
    {
        _mapper = mapper;
        _dbService = dbService;
        _config = config;
    }

    public string Name => "Capterra";

    public void Execute()
    {
        var yaml = File.ReadAllText("capterra.yaml");

        var deserializer = new YamlDotNet.Serialization.Deserializer();
        var capterraProducts = deserializer.Deserialize<List<Capterra>>(yaml);

        if (capterraProducts is null || capterraProducts.Count == 0)
        {
            throw new Exception("File does not have values");
        }

        var errorIndex = ValidateDto(capterraProducts);
        if(errorIndex.Count > 0)
        {
            var indeces = errorIndex.Aggregate("", (res, curr) => $"{res}, {curr}").Substring(2);

            throw new Exception($"Invalid format in following product indeces: {indeces}");
        }

        var products = _mapper.Map<List<Capterra>, List<Product>>(capterraProducts);

        products.ForEach(p =>
        {
            if (_dbService.GetProduct(p.name) is null)
                _dbService.CreateProduct(p);
            else
                _dbService.UpdateProduct(p);
        });
    }

    private List<int> ValidateDto(List<Capterra> products)
    {
        var res = new List<int>();

        for(int i = 0; i < products.Count; i++)
        {
            if(string.IsNullOrEmpty(products[i].name) || string.IsNullOrEmpty(products[i].tags))
            {
                res.Add(i + 1);
            }
        }

        return res;
    }
}