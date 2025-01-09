using Microsoft.EntityFrameworkCore;

public class ProductService : IProductInterface
{
    private MyDbContext context;
    private ServiceResponse<Product> serviceResponse = new ServiceResponse<Product>();

    public ProductService(MyDbContext context)
    {
        this.context = context;
    }

    public async Task<ServiceResponse<Product>> Create(Product product)
    {
        try{
            var repeatedProduct = await context.Products.AnyAsync(p => p.Name == product.Name);

            if(repeatedProduct)
            {
                serviceResponse.Message = "Já existe este produto";
                return serviceResponse;
            }

            context.Products.Add(product);
            await context.SaveChangesAsync();

            serviceResponse.Data = product;
            serviceResponse.Message = "Produto Cadastrado";
        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<Product>>> GetAll()
    {
        ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();
        
        try
        {
            serviceResponse.Data = await context.Products.ToListAsync();

            if(serviceResponse.Data.Count == 0)
            {
                serviceResponse.Message = "Nenhum produto encontrado"; 
            }

        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<Product>> GetById(Guid id)
    {
        try
        {
            serviceResponse.Data = await context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if(serviceResponse.Data == null)
            {
                serviceResponse.Message = "Produto não encontrado";
            }

        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<Product>> Update(Guid id, Product productUpdated)
    {
        try
        {
            var product = await context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if(product == null)
            {
                serviceResponse.Message = "Produto não encontrado";
                return serviceResponse;
            }

            product.Name = productUpdated.Name;
            product.CategoryId = productUpdated.CategoryId;
            product.Price = productUpdated.Price;
            product.Description = productUpdated.Description;
            product.Quantity = productUpdated.Quantity;

            await context.SaveChangesAsync();

        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<Product>> Delete(Guid id)
    {
        try
        {
            serviceResponse.Data = await context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if(serviceResponse.Data == null)
            {
                serviceResponse.Message = "Produto não encontrado";
                return serviceResponse;
            }

            context.Products.Remove(serviceResponse.Data);
            await context.SaveChangesAsync();
        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }
}