using CarteiraDigital.Core.Entities;
using CarteiraDigital.Core.Interface.Repositories;

using CarteiraDigital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarteiraDigital.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet?> GetByUserIdAsync(string userId) =>
            await _context.Wallets
                .Include(w => w.ApplicationUser)
                .FirstOrDefaultAsync(w => w.ApplicationUserId == userId);

        public async Task AddAsync(Wallet wallet) =>
            await _context.Wallets.AddAsync(wallet);

        public Task UpdateAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
