using Domain.Entities;

namespace Application.Services;

public static class UsuarioFactory
{
    public static Usuario Criar(string nome, string email, string senha, DateTime dataNascimento, string? telefone)
    {
        Console.WriteLine("Usuario cadastrado com sucesso! Criando...");
        Usuario usuarioValido = new Usuario();
        usuarioValido.Nome = nome;
        usuarioValido.Email = email.ToLower();
        usuarioValido.Senha = senha;
        usuarioValido.Telefone = telefone;
        usuarioValido.Ativo = true;
        usuarioValido.DataNascimento = dataNascimento;
        usuarioValido.DataCriacao = DateTime.Now;
        return usuarioValido;
    }
}