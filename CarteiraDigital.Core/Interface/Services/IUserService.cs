using CarteiraDigital.Application.DTOs.Users;


namespace CarteiraDigital.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse?> GetByIdAsync(string id);
        Task<UserResponse?> GetByEmailAsync(string email);
    }
}
