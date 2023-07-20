using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace DevIO.App.Configurations;

public static class GlobalizationConfig
{
    public static IApplicationBuilder UseGlobalizationConfig(this IApplicationBuilder app)
    {
        var defaultCuture = new CultureInfo("pt-BR");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCuture),
            SupportedCultures = new List<CultureInfo>{ defaultCuture },
            SupportedUICultures = new List<CultureInfo>{ defaultCuture}
        };
        app.UseRequestLocalization(localizationOptions);
        
        return app;
    }
}