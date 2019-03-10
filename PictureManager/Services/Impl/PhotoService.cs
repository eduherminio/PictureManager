using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using PictureManager.Exceptions;
using PictureManager.Http;
using PictureManager.Model;

namespace PictureManager.Services.Impl
{
    public class PhotoService : IPhotoService
    {
        private readonly PictureManagerConfiguration _pictureManagerConfiguration;
        private readonly HttpClient _httpClient;

        public PhotoService(HttpClient httpClient, PictureManagerConfiguration pictureManagerConfiguration)
        {
            _pictureManagerConfiguration = pictureManagerConfiguration;
            _httpClient = httpClient;
        }

        public ICollection<Photo> LoadAll()
        {
            ICollection<Photo> photoList =
                HttpHelper.Get<ICollection<Photo>>(_httpClient, _pictureManagerConfiguration.PhotoUrl, out HttpStatusCode httpStatusCode);

            if (httpStatusCode != HttpStatusCode.OK || photoList?.Any() == false)
            {
                throw new InternalErrorException(
                    $"Error invoking {_pictureManagerConfiguration?.PhotoUrl}");
            }

            return photoList;
        }
    }
}
