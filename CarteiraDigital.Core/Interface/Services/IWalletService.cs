using CarteiraDigital.Application.DTOs.Wallets;

namespace CarteiraDigital.Application.Interfaces
{
    public interface IWalletService
    {
        Task<WalletBalanceResponse> GetBalanceAsync(string userId);
        Task<AddBalanceResponse> AddBalanceAsync(string userId, AddBalanceRequest request);
    }
}
