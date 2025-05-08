using CarteiraDigital.Core.Entities;
using Microsoft.AspNetCore.Identity;


public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = null!;

    // Relacionamento 1:1 com Wallet
    public virtual Wallet Wallet { get; set; } = null!;
}
