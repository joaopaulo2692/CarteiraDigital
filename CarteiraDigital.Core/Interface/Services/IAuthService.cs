using CarteiraDigital.Application.DTOs.Auth;
using CarteiraDigital.Application.DTOs.Users;


namespace CarteiraDigital.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> AuthenticateAsync(LoginRequest request);
        Task<UserResponse> RegisterAsync(CreateUserRequest request);
    }
}
