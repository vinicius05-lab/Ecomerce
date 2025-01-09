public interface IProductInterface
{
    Task<ServiceResponse<Product>> Create(Product product);
    Task<ServiceResponse<List<Product>>> GetAll();
    Task<ServiceResponse<Product>> GetById(Guid id);
    Task<ServiceResponse<Product>> Update(Guid id, Product productUpdated);
    Task<ServiceResponse<Product>> Delete(Guid id);
}