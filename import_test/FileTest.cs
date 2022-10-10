using import_saas.Models.Dto;
using Newtonsoft.Json;

namespace import_test;

public class FileTest
{

    [Fact]
    public void ReadCapterra()
    {
        var filePath = "/feed-products/capterra.yaml";
        var text = File.ReadAllText(AppContext.BaseDirectory + filePath);

        var deserializer = new YamlDotNet.Serialization.Deserializer();
        var capterraProducts = deserializer.Deserialize<List<Capterra>>(text);

        Console.Write(capterraProducts.First().name);

        Assert.True("GitGHub".Equals(capterraProducts.First().name));
        Assert.True("jira".Equals(capterraProducts[2].twitter));
        Assert.Equal<int>(3, capterraProducts.Count);
    }

    [Fact]
    public void ReadSoftwareAdvice()
    {
        var filePath = "/feed-products/softwareadvice.json";
        var text = File.ReadAllText(AppContext.BaseDirectory + filePath);

        var softwareAdviceWrapper = JsonConvert.DeserializeObject<SoftwareAdvice>(text);

        if (softwareAdviceWrapper == null)
        {
            throw new NullReferenceException();
        }

        var products = softwareAdviceWrapper.products;

        Assert.True("Freshdesk".Equals(products.First().title));
        Assert.Null(products[1].twitter);
        Assert.Equal<int>(2, products.Count());
        Assert.Equal<int>(2, products.First().categories.Count());
    }
}