
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using import_saas.Services;

using IHost host = CreateHostBuilder(args).Build(); 

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((opt, services) => 
        {
            services.AddSingleton<IDbService, DbServices>();
        });
}