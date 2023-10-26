using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TreinamentoAPI.Data;
using TreinamentoAPI.Data.Dtos;
using TreinamentoAPI.Models;

namespace TreinamentoAPI.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("[controller]")]
public class FilmeController : ControllerBase
{
    // Utilizando injeção de dependência para fazer o meio de campo entre o banco e o código:
    // A variável "_context" vai servir como um alvo que acessa o banco de dados.
    private FilmeContext _context;
    // Criando a variável do automapper para fazer o mapeamento com o package automapper:
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    // Enviando dados pelo método post:
    // FromBody está especificando que o dado vem do corpo da aplicação:
    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
    {
        // Mapeando o CreateFilmeDto para que se torne um Filme novamente.
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        // Salvando mudanças no banco:
        _context.SaveChanges();
        // Pegando o caminho da adição do filme:
        return CreatedAtAction(nameof(RecuperarFilmePorId), new { id = filme.Id }, filme);
    }
    // Recebendo os dados pelo método get:
    // Skip serve para pular a quantidade de elementos definidas e Take serve para pegar a quantidade escolhida.
    // 0 é difinido como padrão caso o usuário não queira pular nenhuma quantidade, e 50 é a quantidade padrão que será exibida.
    // FromQuery é para definir que a resposta vai ser na área de pesquisa (URL):
    [HttpGet]
    public IEnumerable<ReadFilmeDto> RetornarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
    }
    // Especificando que o Get vai ser pelo ID, caso tenha sido especificado, caso não especifique o ID, o método Get retorna todos os filmes existentes.
    [HttpGet("{id}")]
    public IActionResult RecuperarFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        // Retornando o status para quando a página não for encontrada por quaisquer motivo:
        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        // Retornando o status para quando o método Get for efetivado:
        return Ok(filmeDto);
    }
    // Atualizando dados pelo método Put:
    // O método Put, serve para atualizar o objeto inteiro.
    // Para atualizar um filme, precisa especificar qual filme será atualizado, por isso colocamos o "ID" dentro do método Put:
    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id,[FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        // Mapeando o novo filme, para que o anterior seja atualizado:
        _mapper.Map(filmeDto, filme);
        // Salvando mudanças no banco:
        _context.SaveChanges();
        // Retornando o status para quando o método Put for efetivado:
        return NoContent();
    }
    // Atualizando dados pelo método Patch:
    // O método Put, serve para atualizar apenas a propriedade selecionada, do objeto.
    // Para atualizar um filme, precisa especificar qual filme será atualizado, por isso colocamos o "ID" dentro do método Put:
    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcialmente(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        // Convertendo o filme do banco, para um updateDTO:
        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
        // Caso o DTO seja válido, o filme é convertido de volta:
        patch.ApplyTo(filmeParaAtualizar, ModelState);
        if (!TryValidateModel(filme))
        {
            return ValidationProblem(ModelState);
        }
        // Caso seja validado, poderemos mapear o novo filme, para que o anterior seja atualizado:
        _mapper.Map(filmeParaAtualizar, filme);
        // Salvando mudanças no banco:
        _context.SaveChanges();
        // Retornando o status para quando o método Put for efetivado:
        return NoContent();
    }
    // Deletando dados pelo método Delete:
    // Para deletar um filme, precisa especificar qual filme será atualizado, por isso colocamos o "ID" dentro do método Delete:
    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
