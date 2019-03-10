using PictureManager.Api.Exceptions;
using PictureManager.Logs;
using PictureManager.Model;
using System.Collections.Generic;

namespace PictureManager.Services
{
    [Log]
    [ExceptionManagement]
    public interface IPhotoService
    {
        ICollection<Photo> LoadAll();
    }
}
