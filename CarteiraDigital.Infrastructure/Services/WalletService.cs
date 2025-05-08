using CarteiraDigital.Application.DTOs.Wallets;
using CarteiraDigital.Application.Interfaces;
using CarteiraDigital.Core.Interface.Repositories;


namespace CarteiraDigital.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _repo;

        public WalletService(IWalletRepository repo)
        {
            _repo = repo;
        }

        public async Task<WalletBalanceResponse> GetBalanceAsync(string userId)
        {
            var wallet = await _repo.GetByUserIdAsync(userId);
            return new WalletBalanceResponse { Balance = wallet?.Balance ?? 0m };
        }

        public async Task<AddBalanceResponse> AddBalanceAsync(string userId, AddBalanceRequest request)
        {
            var wallet = await _repo.GetByUserIdAsync(userId)
                ?? throw new Exception("Carteira não encontrada.");

            wallet.Balance += request.Amount;

            await _repo.UpdateAsync(wallet);
            await _repo.SaveChangesAsync();

            return new AddBalanceResponse { NewBalance = wallet.Balance };
        }
    }
}
