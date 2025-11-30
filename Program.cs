using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>
(options =>
options.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCreateDtoValidator>();


var app = builder.Build();

app.MapGet("/usuarios/", async (IUsuarioService service, CancellationToken ct) =>
{
    var usuarios = await service.ListarAsync(ct);
    return Results.Ok(usuarios);
});

app.MapGet("/usuarios/{id}", async (int id, IUsuarioService service, CancellationToken ct) =>
{
    var usuario = await service.ObterAsync(id, ct);
    if (usuario == null)
        return Results.NotFound();

    return Results.Ok(usuario);
    // return usuario != null ? Results.Ok(usuario) : Results.NotFound();
});

app.MapPost("/usuarios/", async (UsuarioCreateDto dto, IValidator<UsuarioCreateDto> validator, IUsuarioService service, CancellationToken ct) =>
{
    var resultadoValidator = await validator.ValidateAsync(dto, ct);

    if (!resultadoValidator.IsValid)
        return Results.ValidationProblem(resultadoValidator.ToDictionary());

    try
    {
        var usuario = await service.CriarAsync(dto, ct);
        return Results.Created($"/usuarios/{usuario.Id}", usuario);
    }
    catch (ArgumentException ex) when (ex.Message.Contains("email já está sendo usado") || ex.Message.Contains("email já está sendo utilizado"))
    {
        return Results.Conflict(ex.Message);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("/usuarios/{id}", async (int id, UsuarioUpdateDto dto, IValidator<UsuarioUpdateDto> validator, IUsuarioService service, CancellationToken ct) =>
{
    var resultadoValidator = await validator.ValidateAsync(dto, ct);

    if (!resultadoValidator.IsValid)
        return Results.ValidationProblem(resultadoValidator.ToDictionary());

    try
    {
        var usuarioAtualizado = await service.AtualizarAsync(id, dto, ct);
        return Results.Ok(usuarioAtualizado);
    }
    catch (InvalidOperationException ex) when (ex.Message.Contains("email já está sendo usado") || ex.Message.Contains("Este email já está sendo utilizado."))
    {
        return Results.Conflict(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapDelete("/usuarios/{id}", async (int id, IUsuarioService service, CancellationToken ct) =>
{
    var removerUsuario = await service.RemoverAsync(id, ct);

    if (!removerUsuario)
        return Results.NotFound();

    return Results.NoContent();
});



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();