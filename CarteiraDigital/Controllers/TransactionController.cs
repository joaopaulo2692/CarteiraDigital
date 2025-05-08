using CarteiraDigital.Application.DTOs.Transactions;
using CarteiraDigital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarteiraDigital.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        private string GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost]
        public async Task<ActionResult<TransactionResponse>> CreateTransaction(CreateTransactionRequest request)
        {
            var userId = GetUserId();
            var response = await _transactionService.CreateTransactionAsync(userId, request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<TransactionListResponse>> GetTransactions([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = GetUserId();
            var filter = new TransactionFilterRequest
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var response = await _transactionService.GetTransactionsAsync(userId, filter);
            return Ok(response);
        }
    }
}
