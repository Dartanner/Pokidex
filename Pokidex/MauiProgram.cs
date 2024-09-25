using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Pokidex.ViewModels;
using Pokidex.Views;

namespace Pokidex
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseFFImageLoading()
                .UseMauiCommunityToolkitCore()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddDependencies();
            
            return builder.Build();
        }

        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<PokeListVM>();

            services.AddSingleton<PokemonList>();
        }
    }
}
