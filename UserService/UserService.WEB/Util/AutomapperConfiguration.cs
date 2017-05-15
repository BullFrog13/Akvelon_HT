using AutoMapper;
using UserService.BLL.Infrastructure.AutomapperRegistration;
using UserService.WEB.Util.AutomapperProfiles;

namespace UserService.WEB.Util
{
    public class AutomapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoToWcfModelProfile>();
                cfg.AddProfile<EntityToDtoProfile>();
            });

            return mapperConfig;
        }
    }
}
