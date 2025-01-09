public interface IUserInterface
{
    Task<ServiceResponse<User>> Create(User user);
    Task<ServiceResponse<List<User>>> GetAll();
    Task<ServiceResponse<User>> GetById(Guid id);
    Task<ServiceResponse<User>> GetByEmail(string email);
    Task<ServiceResponse<User>> Delete(Guid id);
}