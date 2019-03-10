using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace PictureManager.Api.Test.Utils
{
    public class Fixture : IDisposable
    {
        public static readonly string DefaultString = "testUser";

        public readonly TestServer Server;

        public Fixture()
        {
            IConfiguration configuration = AppSettingsHelpers.GetConfiguration();
            configuration["PhotoUrl"] = "http://jsonplaceholder.typicode.com/photos";
            configuration["AlbumUrl"] = "http://jsonplaceholder.typicode.com/albums";
            Server = TestUtils.CreateTestServer<Startup>(configuration);
        }

        public HttpClient GetClient() => TestUtils.GetHttpClient(Server);

        public TService GetService<TService>()
        {
            return (TService)Server.Host.Services.GetRequiredService(typeof(TService));
        }

        protected virtual void Dispose(bool disposing)
        {
            // Delete DB, if any
            Server.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
