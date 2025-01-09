using System.ComponentModel.DataAnnotations;

public class Category 
{
    [Key]
    public Guid Id {get; init;} = Guid.NewGuid();
    public required string Name {get; set;}
    public Category(string name)
    {
        Name = name;
    }
}