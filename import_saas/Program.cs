
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using import_saas.Services;
using AutoMapper;
using import_saas.Mapper;
using import_saas;

Console.WriteLine("Initializing Program");

using IHost host = CreateHostBuilder(args).Build(); 

using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try 
{
    Console.WriteLine("Starting App");
    services.GetRequiredService<App>().Run(args);
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((opt, services) => 
        {
            IMapper mapper = new MapperConfiguration(opt => {
                opt.AddProfile(new ProductProfile());
            }).CreateMapper();

            services.AddSingleton(mapper);
            services.AddSingleton<IDbService, DbServices>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<App>();
        });
}