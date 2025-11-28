using Domain.Entities;

namespace Application.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync(CancellationToken ct = default);

    Task<Usuario?> GetByIdAsync(int id, CancellationToken ct = default);

    Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct = default);

    Task AddAsync(Usuario usuario, CancellationToken ct = default);

    Task UpdateAsync(Usuario usuario, CancellationToken ct = default);

    Task RemoveAsync(Usuario usuario, CancellationToken ct = default);

    Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);

    Task SaveChangesAsync(CancellationToken ct = default);
}