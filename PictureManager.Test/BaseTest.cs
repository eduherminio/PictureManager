using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using PictureManager.Mapper;

namespace PictureManager.Test
{
    public abstract class BaseTest : IDisposable
    {
        protected IServiceProvider ServiceProvider { get; set; }

        private readonly MapperProvider _mapperProvider;

        protected BaseTest()
        {
            _mapperProvider = new MapperProvider();
        }

        protected IServiceCollection GetBaseServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();

            //services.AddJwtServices();
            if (_mapperProvider != null)
            {
                IMapper Mapper = new AutoMapper.Mapper(CreateMapperConfiguration());
                services.AddSingleton(Mapper);
            }

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
            return ServiceProvider.GetService<T>();
        }

        protected virtual void NewContext()
        {
            NewScope();
        }

        protected MapperConfiguration CreateMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                // DI inside AutoMapper
                cfg.ConstructServicesUsing(Resolve);

                cfg.DisableConstructorMapping();

                // Add all profiles in assembly
                cfg.AddProfiles(_mapperProvider.Assemblies);
            });
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
