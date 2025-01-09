using Microsoft.EntityFrameworkCore;

public class MyDbContext: DbContext {
    public required DbSet<Category> Categories {get; set;}
    public required DbSet<Product> Products {get; set;}
    public required DbSet<User> Users {get; set;}

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) {}
}