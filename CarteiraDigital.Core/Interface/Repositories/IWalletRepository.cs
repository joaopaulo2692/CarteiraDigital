using CarteiraDigital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Core.Interface.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetByUserIdAsync(string userId);
        Task CreateAsync(Wallet wallet);
        Task UpdateAsync(Wallet wallet);
        Task AddAsync(Wallet wallet);
        Task SaveChangesAsync();
    }
}
