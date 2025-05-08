using FluentValidation;
using CarteiraDigital.Application.DTOs.Transactions;

public class CreateTransactionRequestValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionRequestValidator()
    {
        RuleFor(x => x.DestinationUserId)
            .NotEmpty().WithMessage("ID do destinatário é obrigatório.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");
    }
}
