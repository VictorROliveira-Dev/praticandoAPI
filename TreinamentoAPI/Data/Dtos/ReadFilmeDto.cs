namespace TreinamentoAPI.Data.Dtos
{
    public class ReadFilmeDto
    {
        // Classe feita para que as validações do modelo da classe "Filme" não fique exposta e fiquem isoladas dos modelos do banco de dados:
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int Duracao { get; set; }
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    }
}
