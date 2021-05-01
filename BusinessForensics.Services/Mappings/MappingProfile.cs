using AutoMapper;
using BusinessForensics.Domain;
using BusinessForensics.Models;

namespace BusinessForensics.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<GameAttempt, GameAttemptDto>();
        }
    }
}
