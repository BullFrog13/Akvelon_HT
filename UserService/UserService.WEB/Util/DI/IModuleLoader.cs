using Ninject;

namespace UserService.WEB.Util.DI
{
    public interface IModuleLoader
    {
        void LoadAssemblyBindings(IKernel kernel);
    }
}