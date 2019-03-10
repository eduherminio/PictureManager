using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PictureManager.Api.Test.Utils
{
    public static class TestUtils
    {
        public static TestServer CreateTestServer<TStartup>()
            where TStartup : class
        {
            return CreateTestServer<TStartup>(AppSettingsHelpers.GetConfiguration());
        }

        public static TestServer CreateTestServer<TStartup>(IConfiguration configuration)
            where TStartup : class
        {
            return CreateTestServer<TStartup>(configuration, (_) => { });
        }

        public static TestServer CreateTestServer<TStartup>(IConfiguration configuration, Action<IServiceCollection> configureServices)
            where TStartup : class
        {
            var hostBuilder = new WebHostBuilder()
                .UseConfiguration(configuration)
                .ConfigureServices(configureServices)
                .UseStartup<TStartup>();

            return new TestServer(hostBuilder);
        }

        internal static string GetAuthToken(TestServer authServer, string userName)
        {
            var httpClient = GetHttpClient(authServer);
            string tokenUri = $"/api/login?username={userName}";
            string token = HttpHelper.Post(httpClient, tokenUri, string.Empty, out HttpStatusCode statusCode);

            if (statusCode != HttpStatusCode.OK)
            {
                throw new Exception("Login failed");
            }

            return token;
        }

        public static HttpClient GetHttpClient(TestServer server) => server.CreateClient();
    }
}
