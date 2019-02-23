using AutoMapper;
using Projeto.Livaria.Api.Models;
using Projeto.Livraria.Entidades;

namespace Projeto.Livaria.Api.AutoMapper
{
    public class EntidadeToModelMappingProfile : Profile
    {
        public EntidadeToModelMappingProfile()
        {
            CreateMap<Livro, LivroModel>();
        }
    }
}