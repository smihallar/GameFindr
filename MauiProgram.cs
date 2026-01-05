using GameFindr.Services;
using GameFindr.Viewmodels;
using Microsoft.Extensions.Logging;

namespace GameFindr
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<GameService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<MainViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
