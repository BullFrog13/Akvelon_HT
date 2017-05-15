using AutoMapper;
using UserService.BLL.DTO;
using UserService.DAL.Entities;

namespace UserService.BLL.Infrastructure.AutomapperProfiles
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}