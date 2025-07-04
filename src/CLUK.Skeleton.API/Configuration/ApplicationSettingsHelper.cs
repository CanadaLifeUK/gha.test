public static class ApplicationSettingsHelper
{
    private const string ApplicationSettingsConfigurationName = nameof(ApplicationSettings);

    public static ApplicationSettings GetApplicationSettings(this IConfiguration configuration)
    {
        return configuration.GetSection(ApplicationSettingsConfigurationName).Get<ApplicationSettings>() ?? throw new Exception();
    }
}