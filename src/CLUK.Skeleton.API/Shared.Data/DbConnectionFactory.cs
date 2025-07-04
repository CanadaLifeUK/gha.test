namespace CLUK.Skeleton.API.Shared.Data;

public class DbConnectionFactory(ApplicationSettings applicationSettings) : IDbConnectionFactory
{
    private readonly ApplicationSettings applicationSettings = applicationSettings ?? throw new ArgumentNullException(nameof(applicationSettings));

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(applicationSettings.DatabaseSettings.ConnectionString);
    }
}