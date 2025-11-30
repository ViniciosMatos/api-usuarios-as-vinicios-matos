using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repo;

    public UsuarioService(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Usuario>> ListarAsync(CancellationToken ct = default)
    {
        return await _repo.GetAllAsync(ct);
    }

    public async Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct = default)
    {
        if (id <= 0)
            throw new ArgumentException("O ID precisa ser maior que 0.", nameof(id));

        var usuarioExiste = await _repo.GetByIdAsync(id, ct);

        if (usuarioExiste == null)
            return null;

        var usuarioDTO = MappingExtensions.ToReadDto(usuarioExiste);
        return usuarioDTO;
    }

    public async Task<Usuario> CriarAsync(UsuarioCreateDto dto, CancellationToken ct = default)
    {
        var usuario = UsuarioFactory.Criar(dto);
        
        if (await _repo.EmailExistsAsync(dto.Email, ct))
            throw new ArgumentException("O email já está sendo usado por outro usuario.", nameof(dto.Email));

        await _repo.AddAsync(usuario, ct);
        await _repo.SaveChangesAsync(ct);

        return usuario;
    }

    public async Task<Usuario> AtualizarAsync(int  id, UsuarioUpdateDto dto, CancellationToken ct = default)
    {
        if (id <= 0)
            throw new ArgumentException("O ID informado precisa ser maior que 0.", nameof(id));
        
        var usuarioEncontrado = await _repo.GetByIdAsync(id, ct);
        if (usuarioEncontrado == null)
            throw new NotImplementedException("usuario");
        
        if (await _repo.EmailExistsAsync(dto.Email) && dto.Email != usuarioEncontrado.Email)
            throw new InvalidOperationException("Este email já está sendo utilizado.");
        
        usuarioEncontrado.Nome = dto.Nome;
        usuarioEncontrado.Email = dto.Email.ToLower();
        usuarioEncontrado.DataNascimento = dto.DataNascimento;
        usuarioEncontrado.Telefone = dto.Telefone;
        usuarioEncontrado.Ativo = dto.Ativo;
        usuarioEncontrado.DataAtualizacao = DateTime.Now;

        await _repo.UpdateAsync(usuarioEncontrado, ct);
        await _repo.SaveChangesAsync(ct);

        return usuarioEncontrado;
    }

    public async Task<bool> RemoverAsync(int  id, CancellationToken ct = default)
    {
        if (id <= 0)
            throw new ArgumentException("O ID deve ser maior que 0.", nameof(id));
        
        var usuarioEncontrado = await _repo.GetByIdAsync(id, ct);

        if (usuarioEncontrado == null)
            return false;
        
        usuarioEncontrado.Ativo = false;
        await _repo.RemoveAsync(usuarioEncontrado, ct);
        await _repo.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct = default)
    {
        return await _repo.EmailExistsAsync(email, ct);
    }

}