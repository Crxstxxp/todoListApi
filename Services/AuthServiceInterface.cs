using todoListApi.DTOs;

namespace todoListApi.Services
{
    public interface IAuthService
    {
        Task<AuthenticationResult> Register(RegisterRequest request);
        Task<AuthenticationResult> Login(LoginRequest request);
    }
}