

using AutoMapper;
using import_saas.Models.Db.MySql;
using import_saas.Models.Dto;

namespace import_saas.Mapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Capterra, Product>()
            .ForMember(dest => dest.categories, opt => {
                opt.MapFrom(src => src.tags.Split(",", StringSplitOptions.None)
                    .Select(c => new Category(c))
                    .ToList()
                );
            });

        CreateMap<SoftwareAdvice, Product>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.title))
            .ForMember(dest => dest.categories, opt => {
                opt.MapFrom(src => src.categories
                    .Select(c => new Category(c))
                    .ToList()
                );
            });
    }
}