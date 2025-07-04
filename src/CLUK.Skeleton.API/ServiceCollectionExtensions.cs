namespace CLUK.Skeleton.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddScoped<IDbConnectionFactory, DbConnectionFactory>()
            .AddScoped<IDatabaseClient, DatabaseClient>()
            .AddScoped<IPoliciesService, PoliciesService>();

        return services;
    }
}