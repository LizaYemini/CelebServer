using DIContracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DI
{
    public class Resolver : IResolver
    {
        IServiceCollection _serviceCollection;
        IServiceProvider _serviceProvider;
        Dictionary<Policy, Func<Type, Type, IServiceCollection>> _policyFunc;

        private Dictionary<Policy, Func<Type, Type, IServiceCollection>> InitPolicy()
        {
            var retval = new Dictionary<Policy, Func<Type, Type, IServiceCollection>>
            {
                { Policy.Singleton, (interfaceType, type) => _serviceCollection.AddSingleton(interfaceType, type) },
                { Policy.Transient, (interfaceType, type) => _serviceCollection.AddTransient(interfaceType, type) },
                { Policy.Scoped, (interfaceType, type) => _serviceCollection.AddScoped(interfaceType, type) }
            };

            return retval;
        }
        private void InitServiceCollection(string dllpath)
        {

            var files = Directory.GetFiles(dllpath, "*.dll");
            foreach (var file in files)
            {
                Console.WriteLine("File name is:{0}", file);
                var assembly = Assembly.LoadFrom(file);
                foreach (var type in assembly.GetTypes())
                {
                    var attributes = type.GetCustomAttributes<RegisterAttribute>();
                    foreach (var register in attributes)
                    {
                        _serviceCollection = _policyFunc[register.Policy](register.InterfaceType, type);
                    }

                }
            }

        }
        public void LoadLibraries(string path)
        {
            _policyFunc = InitPolicy();
            InitServiceCollection(path);
            _serviceProvider = _serviceCollection.BuildServiceProvider();

        }
        public Resolver(string path, IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
            LoadLibraries(path);
        }
        public Resolver(string path)
        {
            _serviceCollection = new ServiceCollection();

            LoadLibraries(path);


        }
        public IEnumerable<IT> ResolveAll<IT>()
        {
            var retval = _serviceProvider.GetServices<IT>();
            return retval;
        }
        public IT Resolve<IT>()
        {

            IT retval = _serviceProvider.GetService<IT>();
            return retval;
        }

        public object Resolve(Type type)
        {
            var retval = _serviceProvider.GetService(type);
            return retval;
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            var retval = _serviceProvider.GetServices(type);
            return retval;
        }
    }
}
