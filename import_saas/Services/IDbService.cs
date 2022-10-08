using import_saas.Models.Db.MySql;

namespace import_saas.Services;

public interface IDbService
{
    Product? GetProduct(string name);
    bool CreateProduct(Product product);
    bool UpdateProduct(Product product);
}

public class DbServices : IDbService
{
    public Product? GetProduct(string name)
    {
        Console.WriteLine($"Getting product: {name} from database");

        return null;
    }

    public bool CreateProduct(Product product)
    {
        Console.WriteLine($"Saving product {product.name}");

        return true;
    }

    public bool UpdateProduct(Product product)
    {
        Console.WriteLine($"Updating product {product.name}");

        return true;
    }

}