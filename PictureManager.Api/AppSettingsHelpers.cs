using Microsoft.Extensions.Configuration;
using PictureManager.Api.Constants;
using System.IO;

namespace PictureManager.Api
{
    public static class AppSettingsHelpers
    {
        public static IConfiguration GetConfiguration(bool reloadConfigOnChange = false)
        {
            return GetConfigurationFromFile(AppSettingsKeys.ConfigurationFile, reloadConfigOnChange);
        }

        public static IConfiguration GetConfigurationFromFile(string fileName)
        {
            return GetConfigurationFromFile(fileName, false);
        }

        public static IConfiguration GetConfigurationFromFile(string fileName, bool reloadConfigOnChange)
        {
            return string.IsNullOrWhiteSpace(fileName)
                ? GetConfiguration(reloadConfigOnChange)
                : new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(fileName, optional: false, reloadOnChange: reloadConfigOnChange)
                    .AddEnvironmentVariables()
                .Build();
        }
    }
}
