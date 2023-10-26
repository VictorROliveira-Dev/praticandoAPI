using System.ComponentModel.DataAnnotations;

namespace TreinamentoAPI.Data.Dtos;

public class UpdateFilmeDto
{
    // Classe feita para que as validações do modelo da classe "Filme" não fique exposta e fiquem isoladas dos modelos do banco de dados:
    [Required(ErrorMessage = "O título não pode ser nulo ou vazio!")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O gênero do filme é obrigatório!")]
    [StringLength(20, ErrorMessage = "O limite de caracteres para o gênero foi excedido!")]
    public string Genero { get; set; }
    [Range(70, 360, ErrorMessage = "Limite de tempo para o filme excedido!")]
    public int Duracao { get; set; }
}
