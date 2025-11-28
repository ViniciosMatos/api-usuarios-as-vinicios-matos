using Domain.Entities;

namespace Application.Services;

public static class UsuarioFactory
{
    public static Usuario Criar(string nome, string email, string senha, DateTime? dataNascimento, string? telefone)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é inválido...", nameof(nome));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O Email é inválido...", nameof(email));

        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException("Senha inválida...", nameof(senha));

        if (dataNascimento == null)
            throw new ArgumentException("Data de Nascimento inválida...", nameof(dataNascimento));


        Console.WriteLine("Usuario cadastrado com sucesso! Criando...");
        Usuario usuarioValido = new Usuario();
        usuarioValido.Nome = nome;
        usuarioValido.Email = email;
        usuarioValido.Senha = senha;
        usuarioValido.Telefone = telefone;
        usuarioValido.Ativo = true;
        usuarioValido.DataNascimento = dataNascimento;
        usuarioValido.DataCriacao = DateTime.Now;
        return usuarioValido;
    }
}