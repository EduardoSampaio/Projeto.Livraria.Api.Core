using AutoMapper;

namespace Projeto.Livaria.Api.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntidadeToModelMappingProfile());
                cfg.AddProfile(new ModelToEntidadeMappingProfile());
            });
        }
    }
}