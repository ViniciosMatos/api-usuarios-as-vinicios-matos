using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Entities;

public class Usuario
{
    [Key]
    public int Id { get; set; } // PK, Auto-increment

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;// Obrigatório, 3-100 caracteres

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty; // Obrigatório, formato válido, único

    [Required]
    [MinLength(6)]
    public string Senha { get; set; } = string.Empty; // Obrigatório, min 6 caracteres

    [Required]
    public DateTime? DataNascimento { get; set; } // Obrigatório, idade >= 18 anos

    [RegularExpression(@"^$|^\(\d{2}\)\s\d{5}-\d{4}$")]
    public string Telefone { get; set; } = string.Empty; // Opcional, formato (XX) XXXXX-XXXX

    [Required]
    public bool Ativo { get; set; } = true; // Obrigatório, default true

    public DateTime DataCriacao { get; set; } = DateTime.Now;// Obrigatório, preenchido automaticamente

    public DateTime? DataAtualizacao { get; set; } = DateTime.Now; // Opcional, atualizado automaticamente

}