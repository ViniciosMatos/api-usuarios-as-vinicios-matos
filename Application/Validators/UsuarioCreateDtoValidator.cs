using System.Data;
using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
{
    public UsuarioCreateDtoValidator()
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

        RuleFor(u => u.Senha)
        .NotEmpty()
        .WithMessage("A senha não pode ficar em branco")
        .MinimumLength(6)
        .WithMessage("O senha deve ter no mínimo 6 caracteres");

        RuleFor(u => u.DataNascimento)
        .NotEmpty()
        .WithMessage("A data de Nascimento deve ser preenchida");

        RuleFor(u => u.Telefone)
        .Matches(@"^\(\d{2}\)\s\d{5}-\d{4}$")
        .When(u => !string.IsNullOrEmpty(u.Telefone))
        .WithMessage("O telefone deve estar no formato (XX) XXXXX-XXXX");
    }
}