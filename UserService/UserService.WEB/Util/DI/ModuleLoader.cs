using AutoMapper;
using Ninject;
using Ninject.Modules;
using UserService.BLL.Infrastructure;
using UserService.BLL.Interfaces;

namespace UserService.WEB.Util.DI
{
    public class ModuleLoader : IModuleLoader
    {
        public void LoadAssemblyBindings(IKernel kernel)
        {
            var modules = new INinjectModule[] { new ServiceModule("UserDB") };
            kernel.Load(modules);

            kernel.Bind<IUserService>().To<BLL.Services.UserService>();

            var config = new AutomapperConfiguration();
            var mapper = config.Configure().CreateMapper();

            kernel.Bind<IMapper>().ToConstant(mapper).InSingletonScope();
        }
    }
}