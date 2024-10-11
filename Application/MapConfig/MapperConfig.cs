
using Application.ViewModel_And_Dto.Dto.UserSide;
using AutoMapper;
using Doamin.Entities.UserEntities;

namespace Application.MapConfig;

public class MapperConfig : Profile
{

    public MapperConfig()
    {
            CreateMap<User , UserDto>().ReverseMap();
    }

}
