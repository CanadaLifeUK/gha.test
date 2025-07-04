namespace CLUK.Skeleton.API.Shared.Data;

public interface IDatabaseClient
{
    Task<IEnumerable<TResult>> Query<TResult>(string query, object param);
}