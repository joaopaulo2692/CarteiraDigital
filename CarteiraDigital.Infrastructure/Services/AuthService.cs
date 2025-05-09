using CarteiraDigital.Application.DTOs.Auth;
using CarteiraDigital.Application.DTOs.Users;
using CarteiraDigital.Application.Interfaces;

using CarteiraDigital.Core.Entities;
using CarteiraDigital.Core.Interface.Repositories;
using CarteiraDigital.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarteiraDigital.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWalletRepository _walletRepository;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IWalletRepository walletRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _walletRepository = walletRepository;
        }


        public async Task<LoginResponse> AuthenticateAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            var claims = new[]
            {
        // Claim principal em múltiplos formatos
        new Claim(ClaimTypes.NameIdentifier, user.Id),  // Padrão ASP.NET Core
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),  // Padrão JWT
        new Claim("uid", user.Id),  // Fallback customizado
        
        // Claims adicionais
        new Claim(ClaimTypes.Email, user.Email!),
        new Claim(ClaimTypes.Name, user.UserName!),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }


        //    public async Task<LoginResponse> AuthenticateAsync(LoginRequest request)
        //    {
        //        var user = await _userManager.FindByEmailAsync(request.Email);

        //        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        //            throw new UnauthorizedAccessException("Credenciais inválidas.");

        //        var email = user.Email ?? throw new Exception("Usuário sem e-mail cadastrado.");

        //        var authClaims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, user.Id),
        //    new Claim(ClaimTypes.Name, user.Email),
        //    new Claim("username", user.UserName),
        //    new Claim("email", user.Email)
        //};

        //        // Recupera o tempo de expiração a partir da configuração, valor padrão de 20 minutos

        //        int tokenMinutes = 20;

        //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        //        var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512);

        //        var expiration = DateTime.UtcNow.AddMinutes(tokenMinutes);

        //        var token = new JwtSecurityToken(
        //            expires: expiration,
        //            claims: authClaims,
        //            signingCredentials: credentials
        //        );

        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        //        return new LoginResponse
        //        {
        //            Token = tokenString,
        //            Expiration = expiration
        //        };
        //    }


        public async Task<UserResponse> RegisterAsync(CreateUserRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            //return new UserResponse
            //{
            //    Id = Guid.Parse(user.Id),
            //    Name = user.Name,
            //    Email = user.Email
            //};
            if (!result.Succeeded)
                throw new Exception("Erro ao criar usuário: " + string.Join("; ", result.Errors.Select(e => e.Description)));

            // ✅ Criação da carteira automaticamente
            var wallet = new Wallet
            {
                ApplicationUserId = user.Id,
                Balance = 0
            };

            await _walletRepository.CreateAsync(wallet);

            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
