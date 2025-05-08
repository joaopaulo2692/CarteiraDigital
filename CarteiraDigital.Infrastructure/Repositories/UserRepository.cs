using CarteiraDigital.Core.Entities;
using CarteiraDigital.Core.Interface.Repositories;

using CarteiraDigital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarteiraDigital.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id) =>
            await _context.Users.FindAsync(id);

        public async Task<ApplicationUser?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddAsync(ApplicationUser user) =>
            await _context.Users.AddAsync(user);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
