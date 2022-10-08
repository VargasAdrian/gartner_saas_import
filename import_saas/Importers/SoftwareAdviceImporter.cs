using AutoMapper;
using import_saas.Services;
using Microsoft.Extensions.Logging;

namespace import_saas.Importers;

public class SoftwareAdviceImporter : IImporter
{
    private readonly IMapper _mapper;
    private readonly IDbService _dbService;
    private readonly ILogger<SoftwareAdviceImporter> _logger;

    public SoftwareAdviceImporter(
        IMapper mapper,
        IDbService dbService, 
        ILogger<SoftwareAdviceImporter> logger
    )
    {
        _mapper = mapper;
        _dbService = dbService;
        _logger = logger;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}