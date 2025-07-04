namespace CLUK.Skeleton.API.Shared.Data;

public class DatabaseClient(IDbConnectionFactory dbConnectionFactory) : IDatabaseClient
{
    public async Task<IEnumerable<TResult>> Query<TResult>(string query, object param)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<TResult>(query, param);
    }
}