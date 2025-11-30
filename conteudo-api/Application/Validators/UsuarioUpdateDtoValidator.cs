using Application.DTOs;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Validators;

public class UsuarioUpdateDtoValidator : AbstractValidator<UsuarioUpdateDto>
{
    public UsuarioUpdateDtoValidator()
    {   
        RuleFor(u => u.Nome)
        .NotEmpty()
        .WithMessage("O nome não pode ficar em branco")
        .MinimumLength(3)
        .WithMessage("O nome precisa ter no mínimo 3 caracteres")
        .MaximumLength(100)
        .WithMessage("O nome precisa ter no máximo 100 caracteres");

        RuleFor(u => u.Email)
        .NotEmpty()
        .WithMessage("O email não pode ficar em branco")
        .EmailAddress()
        .WithMessage("É um endereço de email: seu@email.com");

        RuleFor(u => u.DataNascimento)
        .NotEmpty()
        .WithMessage("A data de Nascimento deve ser preenchida")
        .Must(d => d <= DateTime.Today.AddYears(-18))
        .WithMessage("O usuário precisa ter 18 anos ou mais");

        RuleFor(u => u.Telefone)
        .Matches(@"^\(\d{2}\)\s\d{5}-\d{4}$")
        .WithMessage("O telefone deve estar no formato (XX) XXXXX-XXXX")
        .When(u => !string.IsNullOrEmpty(u.Telefone));
    }
}