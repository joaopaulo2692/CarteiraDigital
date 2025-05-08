using CarteiraDigital.Application.DTOs.Transactions;
using CarteiraDigital.Application.Interfaces;
using CarteiraDigital.Core.Entities;
using CarteiraDigital.Core.Interface.Repositories;

namespace CarteiraDigital.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IWalletRepository _walletRepo;

        public TransactionService(ITransactionRepository transactionRepo, IWalletRepository walletRepo)
        {
            _transactionRepo = transactionRepo;
            _walletRepo = walletRepo;
        }

        public async Task<TransactionResponse> CreateTransactionAsync(string senderUserId, CreateTransactionRequest request)
        {
            var fromWallet = await _walletRepo.GetByUserIdAsync(senderUserId)
                ?? throw new Exception("Carteira de origem não encontrada.");

            var toWallet = await _walletRepo.GetByUserIdAsync(request.DestinationUserId.ToString())
                ?? throw new Exception("Carteira de destino não encontrada.");

            if (fromWallet.Balance < request.Amount)
                throw new Exception("Saldo insuficiente.");

            fromWallet.Balance -= request.Amount;
            toWallet.Balance += request.Amount;

            var transaction = new Transaction
            {
                Amount = request.Amount,
                FromWalletId = fromWallet.Id,
                ToWalletId = toWallet.Id,
                Timestamp = DateTime.UtcNow
            };

            await _transactionRepo.AddAsync(transaction);
            await _walletRepo.UpdateAsync(fromWallet);
            await _walletRepo.UpdateAsync(toWallet);
            await _walletRepo.SaveChangesAsync();
            await _transactionRepo.SaveChangesAsync();

            return new TransactionResponse
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Timestamp = transaction.Timestamp,
                FromUser = senderUserId,
                ToUser = request.DestinationUserId.ToString()
            };
        }

        public async Task<TransactionListResponse> GetTransactionsAsync(string userId, TransactionFilterRequest filter)
        {
            var list = await _transactionRepo.GetByUserIdAsync(userId, filter.StartDate, filter.EndDate);

            var response = list.Select(t => new TransactionResponse
            {
                Id = t.Id,
                Amount = t.Amount,
                Timestamp = t.Timestamp,
                FromUser = t.FromWallet.ApplicationUserId,
                ToUser = t.ToWallet.ApplicationUserId
            }).ToList();

            return new TransactionListResponse { Transactions = response };
        }
    }
}
