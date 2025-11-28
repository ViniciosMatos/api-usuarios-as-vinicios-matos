using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> ListarAsync(CancellationToken ct = default);

    Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct = default);

    Task<Usuario> CriarAsync(UsuarioCreateDto dto, CancellationToken ct = default);

    Task<Usuario> AtualizarAsync(int id, UsuarioUpdateDto dto, CancellationToken ct = default);

    Task<bool> RemoverAsync(int id, CancellationToken ct = default);

    Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct = default);
}