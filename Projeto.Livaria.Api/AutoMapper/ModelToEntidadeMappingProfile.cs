using AutoMapper;
using Projeto.Livaria.Api.Models;
using Projeto.Livraria.Entidades;

namespace Projeto.Livaria.Api.AutoMapper
{
    public class ModelToEntidadeMappingProfile : Profile
    {
        public ModelToEntidadeMappingProfile()
        {
            CreateMap<LivroModel, Livro>();
        }
    }
}
