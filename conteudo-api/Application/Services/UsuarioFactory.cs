using Application.DTOs;
using Domain.Entities;

namespace Application.Services;

public static class UsuarioFactory
{
    public static Usuario Criar(UsuarioCreateDto dto)
    {
        Console.WriteLine("Usuario cadastrado com sucesso! Criando...");
        Usuario usuarioValido = new Usuario();
        usuarioValido.Nome = dto.Nome;
        usuarioValido.Email = dto.Email.ToLower();
        usuarioValido.Senha = dto.Senha;
        usuarioValido.Telefone = dto.Telefone;
        usuarioValido.Ativo = true;
        usuarioValido.DataNascimento = dto.DataNascimento;
        usuarioValido.DataCriacao = DateTime.Now;
        return usuarioValido;
    }
}