using AutoMapper;
using import_saas.Models.Db.MySql;
using import_saas.Models.Dto;
using import_saas.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace import_saas.Importers;

public class SoftwareAdviceImporter : IImporter
{
    private readonly IMapper _mapper;
    private readonly IDbService _dbService;

    public SoftwareAdviceImporter(
        IMapper mapper,
        IDbService dbService
    )
    {
        _mapper = mapper;
        _dbService = dbService;
    }

    public string Name => "Software Advice";

    public void Execute()
    {
        var json = File.ReadAllText("softwareadvice.json");

        var softwareAdviceProducts = JsonConvert.DeserializeObject<List<SoftwareAdvice>>(json);

        if (softwareAdviceProducts is null || softwareAdviceProducts.Count == 0)
        {
            throw new Exception("File does not have values");
        }

        var products = _mapper.Map<List<SoftwareAdvice>, List<Product>>(softwareAdviceProducts);

        products.ForEach(p =>
        {
            if (_dbService.GetProduct(p.name) is null)
                _dbService.CreateProduct(p);
            else
                _dbService.UpdateProduct(p);
        });
    }
}