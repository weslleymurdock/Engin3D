namespace Engin3D.Auth.Metadata.Extensions;

public static class WebApplicationBuilderExtensions
{
    extension (WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddEngin3DMetadata()
        {
            builder.Services.AddControllers();

            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            return builder;
        }
    }

}
