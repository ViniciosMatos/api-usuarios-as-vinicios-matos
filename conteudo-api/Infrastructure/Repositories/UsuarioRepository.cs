using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Usuario>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Usuarios.ToListAsync(ct);
    }

    public async Task<Usuario?> GetByIdAsync(int  id, CancellationToken ct = default)
    {
        return await _context.Usuarios.FindAsync(id, ct);
    }

    public async Task<Usuario?> GetByEmailAsync(string  email, CancellationToken ct = default)
    {
        return await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == email, ct);
    }

    public async Task AddAsync(Usuario  usuario, CancellationToken ct = default)
    {
        await _context.Usuarios.AddAsync(usuario, ct);
    }

    public Task UpdateAsync(Usuario  usuario, CancellationToken ct = default)
    {
        _context.Usuarios.Update(usuario);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Usuario  usuario, CancellationToken ct = default)
    {
        _context.Usuarios.Update(usuario);
        return Task.CompletedTask;
    }

    public async Task<bool> EmailExistsAsync(string  email, CancellationToken ct = default)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == email, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }

}