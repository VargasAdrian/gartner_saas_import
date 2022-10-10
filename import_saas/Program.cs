
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using import_saas.Services;
using AutoMapper;
using import_saas.Mapper;

using IHost host = CreateHostBuilder(args).Build(); 

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
        });
}