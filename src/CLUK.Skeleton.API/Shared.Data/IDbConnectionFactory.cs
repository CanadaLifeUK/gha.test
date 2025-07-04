using System.Data;

namespace CLUK.Skeleton.API.Shared.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}