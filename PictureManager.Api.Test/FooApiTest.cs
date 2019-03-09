using PictureManager.Api.Test.Utils;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace PictureManager.Api.Test
{
    [Collection(PictureManagerTestCollection.Name)]
    public class FooApiTest
    {
        private readonly Fixture _fixture;

        private const string _fooUri = "api/foo";

        public FooApiTest(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void Add()
        {
            HttpClient client = _fixture.GetClient();
            string foo = Guid.NewGuid().ToString();

            string result = HttpHelper.Post(client, _fooUri, foo, out HttpStatusCode statusCode);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(foo, result);
        }

        [Fact]
        public void Get()
        {
            HttpClient client = _fixture.GetClient();
            string fooId = Guid.NewGuid().ToString();

            string result = HttpHelper.Get<string>(client, $"{_fooUri}/{fooId}", out HttpStatusCode statusCode);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(fooId, result);
        }
    }
}

