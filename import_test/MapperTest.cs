using AutoMapper;
using import_saas.Mapper;
using import_saas.Models.Db.MySql;
using import_saas.Models.Dto;

namespace import_test;

public class MapperTest
{
    private IMapper InitMapper()
    {
        return new MapperConfiguration(opt =>
        {
            opt.AddProfile(new ProductProfile());
        }).CreateMapper();
    }

    [Fact]
    public void CapterraModel()
    {
        var mapper = InitMapper();

        var capterraData = new List<Capterra>()
        {
            new Capterra
            {
                name = "a_name1",
                twitter = "a_twitter1",
                tags = "a_tag1,a_tag2,a_tag3"
            },
            new Capterra
            {
                name = "b_name2",
                twitter = "b_twitter2",
                tags = "b_tag1,b_tag2"
            },
        };

        var products = mapper.Map<List<Capterra>, List<Product>>(capterraData);

        Assert.Equal<int>(3, products.First().categories.Count());
        Assert.True("a_name1".Equals(products.First().name));
        Assert.True("b_twitter2".Equals(products[1].twitter));
    }

    [Fact]
    public void SoftwareAdviceModel()
    {
        var mapper = InitMapper();

        var softwareAdviceData = new SoftwareAdvice()
        {
            products = new List<SoftwareAdviceProduct>()
            {
                new SoftwareAdviceProduct
                {
                    title = "a_title1",
                    categories = new List<string>(){"a_cat1", "a_cat2", "a_cat3"}
                },
                new SoftwareAdviceProduct
                {
                    title = "b_title1",
                    twitter = "b_twitter",
                    categories = new List<string>(){"b_cat1", "b_cat2"}
                },
            }
        };

        var products = mapper.Map<List<SoftwareAdviceProduct>, List<Product>>(softwareAdviceData.products);

        Assert.Equal<int>(3, products.First().categories.Count());
        Assert.Null(products.First().twitter);
        Assert.True("a_title1".Equals(products.First().name));
        Assert.True("b_twitter".Equals(products[1].twitter));
    }
}