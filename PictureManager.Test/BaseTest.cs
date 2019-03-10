using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace PictureManager.Test
{
    public abstract class BaseTest : IDisposable
    {
        protected IServiceProvider ServiceProvider { get; set; }

        protected BaseTest()
        {
            ServiceProvider = GetBaseServiceCollection().BuildServiceProvider();
        }

        protected IServiceCollection GetBaseServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();

            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            IReadOnlyDictionary<string, string> defaultConfigurationStrings =
                new Dictionary<string, string>()
                {
                    ["PhotoUrl"] = "http://jsonplaceholder.typicode.com/photos",
                    ["AlbumUrl"] = "http://jsonplaceholder.typicode.com/albums"
                };

            configurationBuilder.AddInMemoryCollection(defaultConfigurationStrings);

            services.AddPictureManagerServices(configurationBuilder.Build());

            return services;
        }

        private void NewScope()
        {
            IServiceScope service = ServiceProvider.CreateScope();
            ServiceProvider = service.ServiceProvider;
            RenewServices();
        }

        protected abstract void RenewServices();

        protected T GetService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        protected virtual void NewContext()
        {
            NewScope();
        }

        protected object Resolve(Type type)
        {
            ICollection<object> constructorParams = new List<object>();

            ParameterInfo[] parameterInfo = type.GetConstructors().First().GetParameters();
            parameterInfo.ToList().ForEach(p => constructorParams.Add(ServiceProvider.GetService(p.ParameterType)));

            return Activator.CreateInstance(type, constructorParams.ToArray());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) { }
    }
}
