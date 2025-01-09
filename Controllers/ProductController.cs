using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/products")]
public class ProductController
{
    IProductInterface productService;

    public ProductController(IProductInterface productService)
    {
        this.productService = productService;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ServiceResponse<Product>> Create(Product product)
    {
        return await productService.Create(product);
    }

    [HttpGet]
    public async Task<ServiceResponse<List<Product>>> GetAll()
    {
        return await productService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ServiceResponse<Product>> GetById(Guid id)
    {
        return await productService.GetById(id);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ServiceResponse<Product>> Update(Guid id, Product productUpdated)
    {
        return await productService.Update(id, productUpdated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ServiceResponse<Product>> Delete(Guid id)
    {
        return await productService.Delete(id);
    }
}