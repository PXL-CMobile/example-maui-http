using MauiHttp.Services;
using MauiHttp.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MauiHttp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<ActorListViewModel>();
            builder.Services.AddSingleton<FavoriteActorsListViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            
            //builder.Services.AddSingleton<IHTTPService, MOCKHTTPService>();
            builder.Services.AddSingleton<IHTTPService, HTTPService>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<ActorListPage>();
            builder.Services.AddSingleton<FavoriteActorsPage>();

            return builder.Build();
        }
    }
}
