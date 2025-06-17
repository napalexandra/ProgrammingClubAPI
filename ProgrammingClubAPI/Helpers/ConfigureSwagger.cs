using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProgrammingClubAPI.Helpers
{
    public class ConfigureSwagger : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        //provider-ul e important pentru a genera cate un Swagger pentru fiecare versiune din API
        public ConfigureSwagger(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }


        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }


        public void Configure(SwaggerGenOptions options)
        {
            foreach (var item in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(item.GroupName, CreateVersionInfo(item));
            }
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            return new OpenApiInfo
            {
                Title = "Programming Club API",
                Version = description.ApiVersion.ToString(),
                Description = "First API - with authentication from scratch"
            };

        }
    }
}