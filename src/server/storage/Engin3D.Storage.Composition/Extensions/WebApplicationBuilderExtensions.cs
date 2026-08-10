namespace Engin3D.Storage.Composition.Extensions;

public static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddEngin3DStorage()
        {
            builder.Services.AddControllers();

            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            return builder;
        }
    }

}
