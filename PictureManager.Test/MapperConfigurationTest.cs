using Xunit;

using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PictureManager.Test
{
    public class MapperConfigurationTest : BaseTest
    {
        public MapperConfigurationTest()
        {
            IServiceCollection services = new ServiceCollection();

            IMapper Mapper = new AutoMapper.Mapper(CreateMapperConfiguration());
            services.AddSingleton(Mapper);
            ServiceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void MapperConfigurationIsValid()
        {
            GetService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
        }

        protected override void RenewServices() { }
    }
}