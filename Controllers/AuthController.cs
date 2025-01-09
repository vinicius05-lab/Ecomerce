using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/login")]
public class AuthController
{
    AuthService authService;

    public AuthController(AuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost]
    public async Task<ServiceResponse<string>> Login(AuthRequest auth)
    {
        return await authService.Login(auth);
    }
}