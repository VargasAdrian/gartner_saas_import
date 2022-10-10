namespace import_saas.Models.Db.MySql;

using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public int id { get; set; }
    //Assume name is unique in database
    public string name { get; set; }
    public string? twitter { get; set; }

    public IEnumerable<Category> categories { get; set; }
}