using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject;

namespace UserService.WEB.Util.DI
{
    public  class ServiceLocator
    {
        static ServiceLocator()
        {
            Current = new DefaultServiceLocator();
        }

        public static IServiceLocator Current { get; }

        private sealed class DefaultServiceLocator : IServiceLocator
        {
            private readonly IKernel _kernel;

            public DefaultServiceLocator()
            {
                _kernel = new StandardKernel();
                LoadAllAssemblyBindings();
            }

            public T Get<T>()
            {
                return _kernel.Get<T>();
            }

            private void LoadAllAssemblyBindings()
            {
                const string mainAssemblyName = "UserService";
                var loadedAssemblies = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(assembly => assembly.FullName.Contains(mainAssemblyName));

                foreach (var loadedAssembly in loadedAssemblies)
                {
                    var moduleLoaders = GetModuleLoaders(loadedAssembly);
                    foreach (var moduleLoader in moduleLoaders)
                    {
                        moduleLoader.LoadAssemblyBindings(_kernel);
                    }
                }
            }

            private IEnumerable<IModuleLoader> GetModuleLoaders(Assembly loadedAssembly)
            {
                var moduleLoaders = loadedAssembly.GetTypes()
                    .Where(type => type.BaseType != null && (type.GetInterfaces().Contains(typeof(IModuleLoader)) &&
                                                             type.GetConstructor(Type.EmptyTypes) != null))
                    .Select(type => Activator.CreateInstance(type) as IModuleLoader);

                return moduleLoaders;

            }
        }
    }
}