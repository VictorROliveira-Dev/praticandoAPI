using System.ComponentModel.DataAnnotations;

namespace TreinamentoAPI.Models;

public class Filme
{
    // Definindo que o ID é uma chave e é obrigatória:
    [Key]
    [Required]
    public int Id { get; internal set; }
    [Required(ErrorMessage = "O título não pode ser nulo ou vazio!")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O gênero do filme é obrigatório!")]
    [MaxLength(20, ErrorMessage = "O limite de caracteres para o gênero foi excedido!")]
    public string Genero { get; set; }
    [Range(70, 360, ErrorMessage = "Limite de tempo para o filme excedido!")]
    public int Duracao { get; set; }
}
