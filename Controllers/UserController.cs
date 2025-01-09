using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/users")]
public class UserController
{
    IUserInterface userService;

    public UserController(IUserInterface userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    public async Task<ServiceResponse<User>> Create(User user)
    {
        return await userService.Create(user);
    }

    [HttpGet]
    public async Task<ServiceResponse<List<User>>> GetAll()
    {
        return await userService.GetAll();
    }

    [HttpGet("/{email}")]
    public async Task<ServiceResponse<User>> GetByEmail(string email)
    {
        return await userService.GetByEmail(email);
    }

    [HttpGet("/{id:Guid}")]
    public async Task<ServiceResponse<User>> GetById(Guid id)
    {   
        return await userService.GetById(id);
    }

    [HttpDelete("/{id:Guid}")]
    public async Task<ServiceResponse<User>> Delete(Guid id)
    {
        return await userService.Delete(id);
    }
    
}