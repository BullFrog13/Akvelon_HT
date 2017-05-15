using AutoMapper;
using UserService.BLL.DTO;
using UserService.WEB.Models;

namespace UserService.WEB.Util.AutomapperProfiles
{
    public class DtoToWcfModelProfile : Profile
    {
        public DtoToWcfModelProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}