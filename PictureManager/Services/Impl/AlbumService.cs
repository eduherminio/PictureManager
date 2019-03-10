using PictureManager.Exceptions;
using PictureManager.Http;
using PictureManager.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace PictureManager.Services.Impl
{
    public class AlbumService : IAlbumService
    {
        private readonly IPhotoService _photoService;
        private readonly PictureManagerConfiguration _pictureManagerConfiguration;
        private readonly HttpClient _httpClient;

        public AlbumService(IPhotoService photoService, HttpClient httpClient, PictureManagerConfiguration pictureManagerConfiguration)
        {
            _photoService = photoService;
            _pictureManagerConfiguration = pictureManagerConfiguration;
            _httpClient = httpClient;
        }

        public ICollection<Album> LoadAll()
        {
            ICollection<Album> albumList =
                HttpHelper.Get<ICollection<Album>>(_httpClient, _pictureManagerConfiguration.AlbumUrl, out HttpStatusCode httpStatusCode);

            if (httpStatusCode != HttpStatusCode.OK || albumList == null)
            {
                throw new InternalErrorException(
                    $"Error invoking {_pictureManagerConfiguration?.AlbumUrl}");
            }

            ICollection<Photo> photoList = _photoService.LoadAll();

            foreach (var photosInACertainAlbum in photoList.GroupBy(photo => photo.AlbumId))
            {
                albumList.SingleOrDefault(album => album.Id == photosInACertainAlbum.Key)
                    ?.Photos.AddRange(photosInACertainAlbum);
            }

            return albumList;
        }

        public ICollection<Album> FindByUser(string userId)
        {
            return LoadAll().Where(album => album.UserId == userId).ToList();
        }
    }
}
