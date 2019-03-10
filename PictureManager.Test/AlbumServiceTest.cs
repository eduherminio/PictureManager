using PictureManager.Exceptions;
using PictureManager.Model;
using PictureManager.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace PictureManager.Test
{
    public class AlbumServiceTest : BaseTest
    {
        private IAlbumService _albumService;

        public AlbumServiceTest()
        {
            RenewServices();
        }

        protected override void RenewServices()
        {
            _albumService = GetService<IAlbumService>();
        }

        [Fact]
        public void LoadAll()
        {
            ICollection<Album> albumList = _albumService.LoadAll();

            Assert.NotEmpty(albumList);
            foreach (Album album in albumList)
            {
                ValidateAlbum(album);
                foreach (Photo photo in album.Photos)
                {
                    ValidatePhoto(photo);
                    Assert.Equal(album.Id, photo.AlbumId);
                }
            }
        }

        [Fact]
        public void FindByUserId()
        {
            const string testId = "3";

            ICollection<Album> albumList = _albumService.FindByUser(testId);

            Assert.NotEmpty(albumList);
            foreach (Album album in albumList)
            {
                ValidateAlbum(album);
                Assert.Equal(album.UserId, testId);

                foreach (Photo photo in album.Photos)
                {
                    ValidatePhoto(photo);
                }
            }
        }

        [Fact]
        public void FindByUnknownUserId()
        {
            const string testId = "12345678";

            ICollection<Album> albumList = _albumService.FindByUser(testId);

            Assert.Empty(albumList);
        }

        [Fact]
        public void ShouldNotBeCapableOfGettingData()
        {
            GetService<PictureManagerConfiguration>().AlbumUrl = new Uri("http://google.es");
            Assert.Throws<InternalErrorException>(() => _albumService.LoadAll());

            NewContext();
            GetService<PictureManagerConfiguration>().PhotoUrl = new Uri("http://google.au");
            Assert.Throws<InternalErrorException>(() => _albumService.LoadAll());

            NewContext();
            GetService<PictureManagerConfiguration>().AlbumUrl = new Uri("http://jsonplaceholder.typicode.com/albums");
            GetService<PictureManagerConfiguration>().PhotoUrl = new Uri($"http://{Guid.NewGuid()}.com");
            Assert.Throws<AggregateException>(() => _albumService.LoadAll());
        }

        private static void ValidateAlbum(Album album)
        {
            Assert.False(string.IsNullOrWhiteSpace(album.Id));
            Assert.False(string.IsNullOrWhiteSpace(album.UserId));
            Assert.Null(album.User);
        }

        private static void ValidatePhoto(Photo photo)
        {
            Assert.False(string.IsNullOrWhiteSpace(photo.AlbumId));
            Assert.False(string.IsNullOrWhiteSpace(photo.Title));
            Assert.False(string.IsNullOrWhiteSpace(photo.Url.OriginalString));
            Assert.False(string.IsNullOrWhiteSpace(photo.ThumbnailUrl.OriginalString));
        }
    }
}
