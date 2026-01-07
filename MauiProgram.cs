using GameFindr.Pages;
using GameFindr.Services;
using GameFindr.Viewmodels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace GameFindr
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Typographica.ttf", "Typographica");
                });


            builder.Services.AddHttpClient<GameService>(client =>
            {
                var baseUrl = "https://api.gamebrain.co/v1/games/";
                var apiKey = "8326cb610c964c23ae992ddfd7030a5b";
                client.BaseAddress = new Uri(baseUrl);
                if (!string.IsNullOrWhiteSpace(apiKey))
                {
                    client.DefaultRequestHeaders.Add("x-api-key", apiKey);
                }
            });

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<GameListPage>();
            builder.Services.AddTransient<GameListViewModel>();
            builder.Services.AddTransient<GameDetailsPage>();
            builder.Services.AddTransient<GameDetailsViewModel>();
            builder.Services.AddTransient<SimilarGamesViewModel>();
            builder.Services.AddTransient<SimilarGamesPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
