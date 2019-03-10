using PictureManager.Model;
using System.Collections.Generic;

namespace PictureManager.Services
{
    public interface IAlbumService
    {
        ICollection<Album> FindByUser(string userId);

        ICollection<Album> LoadAll();
    }
}
