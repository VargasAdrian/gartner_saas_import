namespace import_saas.Models.Db.MySql;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    [Key]
    public int? id { get; set; }

    [ForeignKey(nameof(Product))]
    public int? product_id { get; set; }
    public string name { get; set; } = "";

    public Category(string name)
    {
        this.name = name;
    }
}