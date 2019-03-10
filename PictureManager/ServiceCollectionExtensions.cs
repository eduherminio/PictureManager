using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PictureManager.Services;
using PictureManager.Services.Impl;

namespace PictureManager
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPictureManagerServices(this IServiceCollection services, IConfiguration configuration)
        {
            PictureManagerConfiguration pictureManagerConfiguration = new PictureManagerConfiguration();
            configuration.Bind(pictureManagerConfiguration);
            services.AddSingleton(pictureManagerConfiguration);

            services.AddScoped<IAlbumService, AlbumService>();
            services.AddHttpClient<IAlbumService, AlbumService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddHttpClient<IPhotoService, PhotoService>();
        }
    }
}
