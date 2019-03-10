using Microsoft.AspNetCore.Mvc;
using PictureManager.Model;
using PictureManager.Services;
using System.Collections.Generic;

namespace PictureManager.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/albums")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public ICollection<Album> Load()
        {
            return _albumService.LoadAll();
        }

        [HttpGet("{userId}")]
        public ICollection<Album> Load(string userId)
        {
            return _albumService.FindByUser(userId);
        }
    }
}
