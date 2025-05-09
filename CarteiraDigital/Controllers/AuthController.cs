﻿using CarteiraDigital.Application.DTOs.Auth;
using CarteiraDigital.Application.DTOs.Users;
using CarteiraDigital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraDigital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        /// <summary>
        /// Realiza o login do usuário e retorna um token JWT.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var response = await _authService.AuthenticateAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno no servidor", details = ex.Message });
            }
        }


        /// <summary>
        /// Realiza o registro de usuário.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            try
            {
                var response = await _authService.RegisterAsync(request);
                return CreatedAtAction(nameof(Register), new { id = response.Id }, response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno no servidor", details = ex.Message });
            }
        }

        /// <summary>
        /// Testes de Autenticação
        /// </summary>
        [AllowAnonymous]
        [HttpGet("test-auth")]
        public IActionResult TestAuth()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"];
            return Ok(new { authHeader });
        }
    }
}
