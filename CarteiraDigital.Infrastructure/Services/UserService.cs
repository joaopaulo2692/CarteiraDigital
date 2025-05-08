using CarteiraDigital.Application.DTOs.Users;
using CarteiraDigital.Application.Interfaces;
using CarteiraDigital.Core.Interface.Repositories;

namespace CarteiraDigital.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserResponse?> GetByIdAsync(string id)
        {
            var user = await _repo.GetByIdAsync(id);
            return user == null ? null : new UserResponse
            {
                Id = Guid.Parse(user.Id),
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<UserResponse?> GetByEmailAsync(string email)
        {
            var user = await _repo.GetByEmailAsync(email);
            return user == null ? null : new UserResponse
            {
                Id = Guid.Parse(user.Id),
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
