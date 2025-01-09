using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product 
{
    [Key]
    public Guid Id {get; init;} = Guid.NewGuid();
    public required string Name {get; set;}
    [ForeignKey("CategoryId")]
    public required Guid CategoryId {get; set;}
    public Category? Category {get; set;}
    public required float Price {get; set;}
    public required string Description {get; set;}
    public required int Quantity {get; set;}

    public Product(string name, Guid categoryId, float price, string description, int quantity)
    {
        Name = name;
        CategoryId = categoryId;
        Price = price;
        Description = description;
        Quantity = quantity;
    }
}