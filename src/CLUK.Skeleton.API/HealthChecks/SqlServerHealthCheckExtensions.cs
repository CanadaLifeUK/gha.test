using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace CLUK.Skeleton.API.HealthChecks;

public static class SqlServerHealthCheckExtensions
{
    private const string HealthCheckName = "sql-server";

    public static IHealthChecksBuilder AddSqlServerHealthCheck(
        this IHealthChecksBuilder healthChecksBuilder,
        ApplicationSettings applicationSettings)
    {
        healthChecksBuilder
            .AddSqlServer(
                connectionString: applicationSettings.DatabaseSettings.ConnectionString,
                failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
                healthQuery: "SELECT 1;",
                name: HealthCheckName,
                tags: new[] { "db", "sql", HealthCheckName });

        return healthChecksBuilder;
    }
}