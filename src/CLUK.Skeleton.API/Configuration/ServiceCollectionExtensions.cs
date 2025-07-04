public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationSettings(this IServiceCollection services, ApplicationSettings applicationSettings)
    {
        services.AddScoped(_ => applicationSettings!);

        return services;
    }
}