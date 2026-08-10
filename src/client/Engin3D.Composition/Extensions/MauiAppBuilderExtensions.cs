using CommunityToolkit.Maui;

namespace Engin3D.Composition.Extensions;

// All the code in this file is included in all platforms.
public static class MauiAppBuilderExtensions
{
    extension (MauiAppBuilder builder)
    {
        public MauiAppBuilder AddEngin3D()
        { 
            return builder;
        }
    }

}
