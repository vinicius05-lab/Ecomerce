using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public Guid Id {get; init;} = Guid.NewGuid();
    public required string Name {get; set;}
    public required string Email {get; set;}
    public required string Password {get; set;}
    public required UserRole Role {get; set;} = UserRole.ADMIN;

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}