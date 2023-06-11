using AutoMapper;
using MusicMarket.Api.DTO;
using MusicMarket.Core.Models;

namespace MusicMarket.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Music, MusicDTO>().ReverseMap();
            CreateMap<Music, SaveMusicDTO>().ReverseMap();

            CreateMap<Artist, ArtistDTO>().ReverseMap();
            CreateMap<Artist, SaveArtistDTO>().ReverseMap();
        }
    }
}
