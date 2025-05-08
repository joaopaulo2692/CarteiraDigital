using CarteiraDigital.Application.DTOs.Wallets;
using CarteiraDigital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarteiraDigital.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        private string GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet("balance")]
        public async Task<ActionResult<WalletBalanceResponse>> GetBalance()
        {
            var userId = GetUserId();
            var response = await _walletService.GetBalanceAsync(userId);
            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<ActionResult<AddBalanceResponse>> AddBalance(AddBalanceRequest request)
        {
            var userId = GetUserId();
            var response = await _walletService.AddBalanceAsync(userId, request);
            return Ok(response);
        }
    }
}
