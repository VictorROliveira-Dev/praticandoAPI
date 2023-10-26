using AutoMapper;
using TreinamentoAPI.Data.Dtos;
using TreinamentoAPI.Models;

namespace TreinamentoAPI.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        // Convertendo o CreateFilmeDto em um filme com o automapper:
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, UpdateFilmeDto>();
        CreateMap<Filme, ReadFilmeDto>();
    }
}
