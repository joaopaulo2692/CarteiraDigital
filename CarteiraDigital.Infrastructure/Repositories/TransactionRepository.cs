using CarteiraDigital.Core.Entities;
using CarteiraDigital.Core.Interface.Repositories;
using CarteiraDigital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarteiraDigital.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction) =>
            await _context.Transactions.AddAsync(transaction);

        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = _context.Transactions
                .Include(t => t.FromWallet)
                .Include(t => t.ToWallet)
                .Where(t => t.FromWallet.ApplicationUserId == userId || t.ToWallet.ApplicationUserId == userId);

            if (fromDate.HasValue)
                query = query.Where(t => t.Timestamp >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(t => t.Timestamp <= toDate.Value);

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
