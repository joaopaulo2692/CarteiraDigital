using CarteiraDigital.Application.DTOs.Wallets;
using CarteiraDigital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarteiraDigital.API.Controllers
{
    [ApiController]
    //[Authorize]
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

        /// <summary>
        /// Obtém o saldo da carteira do usuário autenticado.
        /// </summary>
        [HttpGet("balance")]
        public async Task<ActionResult<WalletBalanceResponse>> GetBalance()
        {
            try
            {
                var userId = GetUserId();
                var response = await _walletService.GetBalanceAsync(userId);

                if (response == null)
                    return NotFound(new { message = "Carteira não encontrada." });

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro ao consultar saldo." });
            }
        }

        /// <summary>
        /// Adiciona saldo à carteira do usuário autenticado.
        /// </summary>
        [HttpPost("add")]
        public async Task<ActionResult<AddBalanceResponse>> AddBalance(AddBalanceRequest request)
        {
            try
            {
                var userId = GetUserId();
                var response = await _walletService.AddBalanceAsync(userId, request);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro ao adicionar saldo." });
            }
        }
    }
}
