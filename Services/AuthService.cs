using Microsoft.EntityFrameworkCore;

public class AuthService
{
    MyDbContext context;

    public AuthService(MyDbContext context)
    {
        this.context = context;
    }

    public async Task<ServiceResponse<string>> Login(AuthRequest auth)
    {
        ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

        try
        {
            if(auth.Email != null && auth.Password != null)
            {
                var user = await context.Users.SingleOrDefaultAsync(u => u.Email == auth.Email);
                if(user == null)
                {
                    serviceResponse.Message = "Não foi encontrado usuário com este email";
                    return serviceResponse;
                } else if(!BCrypt.Net.BCrypt.Verify(auth.Password, user.Password))
                {
                    serviceResponse.Message = "Senha incorreta";
                    return serviceResponse;
                }

                serviceResponse.Data = TokenService.GenerateToken(user);
            } else 
            {
                serviceResponse.Message = "Email e Senha devem ser preenchidos";
            }
        } catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }
}