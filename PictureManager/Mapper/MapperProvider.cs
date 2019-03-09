using AutoMapper;
using System.Collections.Generic;
using System.Reflection;

namespace PictureManager.Mapper
{
    public interface IMapperProvider
    {
        ICollection<Assembly> Assemblies { get; }
    }

    public class MapperProvider : IMapperProvider
    {
        public ICollection<Assembly> Assemblies { get; private set; }

        protected virtual MapperConfiguration CreateConfiguration()
        {
            return new MapperConfiguration(_ => { });
        }

        public MapperProvider()
        {
            Assemblies = new List<Assembly>();
        }

        public AutoMapper.Mapper GetMapper()
        {
            return new AutoMapper.Mapper(CreateConfiguration());
        }
    }
}
