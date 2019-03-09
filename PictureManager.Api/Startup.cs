using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PictureManager.Api.Exceptions;
using PictureManager.Mapper;
using PictureManager.Api.Swagger;

namespace PictureManager.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private static readonly ApiVersion _apiVersion = new ApiVersion(1, 0);

        private ApiInfo _apiInfo => new ApiInfo(
            "PictureManager API",
            "API for managing pictures",
            _apiVersion);

        private string _swaggerDocumentVersion { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _swaggerDocumentVersion = $"v{_apiInfo.ApiVersion}";
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddOptions();

            //services.AddJwtServices();

            // Api versioning
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = _apiInfo.ApiVersion;
            });

            // Swagger
            string assemblyName = GetType().GetTypeInfo().Assembly.GetName().Name;
            services.ConfigureSwaggerMvcServices(_swaggerDocumentVersion, _apiInfo, assemblyName);

            // AutoMapper
            services.ConfigureAutoMapper();

            services.AddAuthenticationCore();
            services.AddAuthorizationPolicyEvaluator();

            services.AddMvcCore().AddApiExplorer();
            services.AddMvc(
                options => options.Filters.Add(typeof(ExceptionFilterAttribute)))
                .AddJsonOptions(options => options.SerializerSettings.AddCustomJsonSerializerSettings());

            services.AddPictureManagerServices();

            return services.BuildAspectInjectorProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureSwaggerMvc(_swaggerDocumentVersion);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
