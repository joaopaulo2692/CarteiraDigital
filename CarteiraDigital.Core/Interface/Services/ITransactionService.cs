using CarteiraDigital.Application.DTOs.Transactions;


namespace CarteiraDigital.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResponse> CreateTransactionAsync(string senderUserId, CreateTransactionRequest request);
        Task<TransactionListResponse> GetTransactionsAsync(string userId, TransactionFilterRequest filter);
    }
}
