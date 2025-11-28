using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repo;

    public UsuarioService(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Usuario>> ListarAsync(CancellationToken ct)
    {
        return await _repo.GetAllAsync(ct);
    }

    public async Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct)
    {
        if (id <= 0)
            throw new ArgumentException("O ID precisa ser maior que 0.");

        var usuarioExiste = await _repo.GetByIdAsync(id, ct);

        if (usuarioExiste == null)
            return null;

        var usuarioDTO = MappingExtensions.ToReadDto(usuarioExiste);
        return usuarioDTO;
    }

    public async Task<Usuario> CriarAsync(UsuarioCreateDto dto, CancellationToken ct)
    {
        var usuario = UsuarioFactory.Criar(dto.Nome, dto.Email, dto.Senha, dto.DataNascimento, dto.Telefone);

        if (string.IsNullOrEmpty(usuario.Nome))
            throw new ArgumentException("O nome não pode ficar vazio.");

        if (string.IsNullOrEmpty(usuario.Email))
            throw new ArgumentException("O Email não pode ficar vazio.");

        if (string.IsNullOrEmpty(usuario.Senha))
            throw new ArgumentException("A senha não pode ficar em branco.");

        if (usuario.DataNascimento == null)
            throw new ArgumentException("A Data de Nascimento é obrigatória.");
        
        if (await _repo.EmailExistsAsync(usuario.Email, ct))
            throw new ArgumentException("O email já está sendo usado por outro usuario.");

        await _repo.AddAsync(usuario, ct);
        await _repo.SaveChangesAsync(ct);

        return usuario;
    }

    public async Task<Usuario> AtualizarAsync(int  id, UsuarioUpdateDto  dto, CancellationToken ct)
    {
        if (id <= 0)
            throw new ArgumentException("O ID informado precisa ser maior que 0.");
        
        var usuarioEncontrado = await _repo.GetByIdAsync(id, ct);
        if (usuarioEncontrado == null)
            throw new ArgumentException("O usuario não foi encontrado");
        
        if (dto.Email != usuarioEncontrado.Email && await _repo.EmailExistsAsync(dto.Email, ct))
            throw new ArgumentException("Este email já está sendo utilizado.");
        
        usuarioEncontrado.Nome = dto.Nome;
        usuarioEncontrado.Email = dto.Email;
        usuarioEncontrado.DataNascimento = dto.DataNascimento;
        usuarioEncontrado.Telefone = dto.Telefone;
        usuarioEncontrado.DataAtualizacao = DateTime.Now;

        await _repo.UpdateAsync(usuarioEncontrado, ct);
        await _repo.SaveChangesAsync(ct);

        return usuarioEncontrado;
    }

    public async Task<bool> RemoverAsync(int  id, CancellationToken ct)
    {
        if (id <= 0)
            throw new ArgumentException("O ID deve ser maior que 0.");
        
        var usuarioEncontrado = await _repo.GetByIdAsync(id, ct);

        if (usuarioEncontrado == null)
            return false;
        
        usuarioEncontrado.Ativo = false;
        await _repo.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct)
    {
        return await _repo.EmailExistsAsync(email, ct);
    }





}