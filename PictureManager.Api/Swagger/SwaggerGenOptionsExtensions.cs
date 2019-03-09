using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace PictureManager.Api.Swagger
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void AddJwtAuthentication(this SwaggerGenOptions c)
        {
            c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });

            c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = "header",
                Type = "apiKey"
            });
        }
    }
}
