public interface ICategoryInterface
{
    Task<ServiceResponse<Category>> Create(Category category);
    Task<ServiceResponse<List<Category>>> GetAll();
    Task<ServiceResponse<Category>> GetById(Guid id);
    Task<ServiceResponse<Category>> Update(Guid id, Category categoryUpdated);
    Task<ServiceResponse<Category>> Delete(Guid id);
}