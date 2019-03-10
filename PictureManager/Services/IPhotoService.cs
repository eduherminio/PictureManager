using PictureManager.Model;
using System.Collections.Generic;

namespace PictureManager.Services
{
    public interface IPhotoService
    {
        ICollection<Photo> LoadAll();
    }
}
