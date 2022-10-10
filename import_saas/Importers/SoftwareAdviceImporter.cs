using AutoMapper;
using import_saas.Models.Db.MySql;
using import_saas.Models.Dto;
using import_saas.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace import_saas.Importers;

public class SoftwareAdviceImporter : IImporter
{
    private readonly IMapper _mapper;
    private readonly IDbService _dbService;
    private readonly IConfiguration _config;

    public SoftwareAdviceImporter(
        IMapper mapper,
        IDbService dbService,
        IConfiguration config
    )
    {
        _mapper = mapper;
        _dbService = dbService;
        _config = config;
    }

    public string Name => "Software Advice";

    public void Execute()
    {
        var json = File.ReadAllText("softwareadvice.json");

        var softwareAdviceWrapper = JsonConvert.DeserializeObject<SoftwareAdvice>(json);

        if (softwareAdviceWrapper is null || softwareAdviceWrapper.products is null || softwareAdviceWrapper.products.Count == 0)
        {
            throw new Exception("File does not have values");
        }

        var saProducts = softwareAdviceWrapper.products;

        var errorIndex = ValidateDto(saProducts);
        if (errorIndex.Count > 0)
        {
            var indeces = errorIndex.Aggregate("", (res, curr) => $"{res}, {curr}").Substring(2);

            throw new Exception($"Invalid format in following product indeces: {indeces}");
        }

        var products = _mapper.Map<List<SoftwareAdviceProduct>, List<Product>>(saProducts);

        products.ForEach(p =>
        {
            if (_dbService.GetProduct(p.name) is null)
                _dbService.CreateProduct(p);
            else
                _dbService.UpdateProduct(p);
        });
    }

    private List<int> ValidateDto(List<SoftwareAdviceProduct> products)
    {
        var res = new List<int>();

        for (int i = 0; i < products.Count; i++)
        {
            if (string.IsNullOrEmpty(products[i].title) || products[i].categories is null || products[i].categories.Count == 0)
            {
                res.Add(i + 1);
            }
        }

        return res;
    }
}