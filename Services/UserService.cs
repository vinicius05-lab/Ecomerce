using Microsoft.EntityFrameworkCore;

public class UserService : IUserInterface
{
    MyDbContext context;

    public UserService(MyDbContext context)
    {
        this.context = context;
    }

    public async Task<ServiceResponse<User>> Create(User user)
    {
        ServiceResponse<User> serviceResponse = new ServiceResponse<User>();

        try
        {
            var repeatedUser = await context.Users.AnyAsync(u => u.Email == user.Email);

            if(repeatedUser)
            {
                serviceResponse.Message = "Já tem usuário com este email cadastrado";
                return serviceResponse;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            serviceResponse.Data = user;
            serviceResponse.Message = "Usuário cadastrado";
        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<User>>> GetAll()
    {
        ServiceResponse<List<User>> serviceResponse = new ServiceResponse<List<User>>();

        try
        {
            serviceResponse.Data = await context.Users.ToListAsync();

            if(serviceResponse.Data.Count == 0)
            {
                serviceResponse.Message = "Nenhum usuário encontrado";
            }
        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<User>> GetByEmail(string email)
    {
        ServiceResponse<User> serviceResponse = new ServiceResponse<User>();

        try
        {
            serviceResponse.Data = await context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if(serviceResponse.Data == null)
            {
                serviceResponse.Message = "Usuário não encontrado";
            }

        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<User>> GetById(Guid id)
    {
        ServiceResponse<User> serviceResponse = new ServiceResponse<User>();

        try
        {
            serviceResponse.Data = await context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if(serviceResponse.Data == null)
            {
                serviceResponse.Message = "Usuário não encontrado";
            }
            
        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }
    
    public async Task<ServiceResponse<User>> Delete(Guid id)
    {
        ServiceResponse<User> serviceResponse = new ServiceResponse<User>();

        try
        {
            serviceResponse.Data = await context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if(serviceResponse.Data == null)
            {
                serviceResponse.Message = "Usuário não encontrado";
                return serviceResponse;
            }

            context.Users.Remove(serviceResponse.Data);
            await context.SaveChangesAsync();

        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }
}