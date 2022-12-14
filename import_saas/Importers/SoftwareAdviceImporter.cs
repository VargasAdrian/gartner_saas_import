using AutoMapper;
using import_saas.Models.Db.MySql;
using import_saas.Models.Dto;
using import_saas.Services;
using Newtonsoft.Json;

namespace import_saas.Importers;

public class SoftwareAdviceImporter : IImporter
{
    private readonly IMapper _mapper;
    private readonly IDbService _dbService;
    private readonly IFileService _fileService;

    public SoftwareAdviceImporter(
        IMapper mapper,
        IDbService dbService,
        IFileService fileService
    )
    {
        _mapper = mapper;
        _dbService = dbService;
        _fileService = fileService;
    }

    public string Name => "Software Advice";

    public void Execute()
    {
        var text = File.ReadAllText(_fileService.SoftwareAdvice);

        var softwareAdviceWrapper = JsonConvert.DeserializeObject<SoftwareAdvice>(text);

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