

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

        CreateMap<SoftwareAdviceProduct, Product>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.title))
            .ForMember(dest => dest.twitter, opt => opt.AddTransform(val => val == null ? null : val.Replace("@", "")))
            .ForMember(dest => dest.categories, opt => {
                opt.MapFrom(src => src.categories == null ? null : src.categories
                    .Select(c => new Category(c))
                    .ToList()
                );
            });
    }
}