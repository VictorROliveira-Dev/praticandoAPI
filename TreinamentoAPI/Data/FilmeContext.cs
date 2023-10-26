using Microsoft.EntityFrameworkCore;
using TreinamentoAPI.Models;

namespace TreinamentoAPI.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts)
    {
    }
    // Propriedade para salvar as coisas dentro do banco:
    public DbSet<Filme> Filmes { get; set; }
}
