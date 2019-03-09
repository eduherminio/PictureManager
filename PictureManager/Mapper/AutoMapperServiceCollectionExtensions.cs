using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PictureManager.Mapper
{
    public static class AutoMapperServiceCollectionExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            List<Assembly> assemblies = new List<Assembly>();
            var serviceProvider = services.BuildServiceProvider();
            var providers = serviceProvider.GetRequiredService<IEnumerable<IMapperProvider>>();
            foreach (IMapperProvider provider in providers)
            {
                assemblies.AddRange(provider.Assemblies);
            }

            services.AddSingleton(new MapperProvider());
            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IMapperProvider));
            services.Remove(serviceDescriptor);
        }
    }
}
