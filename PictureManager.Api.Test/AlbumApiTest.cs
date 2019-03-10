using PictureManager.Api.Test.Utils;
using PictureManager.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace PictureManager.Api.Test
{
    [Collection(PictureManagerTestCollection.Name)]
    public class AlbumApiTest
    {
        private readonly Fixture _fixture;

        private const string _albumUri = "api/albums";

        public AlbumApiTest(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void LoadAll()
        {
            HttpClient client = _fixture.GetClient();

            ICollection<Album> result = HttpHelper.Get<ICollection<Album>>(client, _albumUri, out HttpStatusCode statusCode);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void FindByUserId()
        {
            HttpClient client = _fixture.GetClient();
            const string userId = "5";

            ICollection<Album> result = HttpHelper.Get<ICollection<Album>>(client, $"{_albumUri}/{userId}", out HttpStatusCode statusCode);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.NotEmpty(result);
        }
    }
}

