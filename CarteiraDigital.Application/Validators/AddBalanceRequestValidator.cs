using FluentValidation;
using CarteiraDigital.Application.DTOs.Wallets;

public class AddBalanceRequestValidator : AbstractValidator<AddBalanceRequest>
{
    public AddBalanceRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");
    }
}
