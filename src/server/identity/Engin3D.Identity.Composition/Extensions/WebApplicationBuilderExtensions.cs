namespace Engin3D.Identity.Composition.Extensions;

public static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddEngin3DIdentity()
        {
            builder.Services.AddControllers();

            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            return builder;
        }
    }

}
