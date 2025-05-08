using CarteiraDigital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Core.Interface.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId, DateTime? fromDate = null, DateTime? toDate = null);
        Task SaveChangesAsync();
    }
}
