namespace CLUK.Skeleton.API.Configuration;

public record DatabaseSettings(string DataSource, string DataSourceProperties, string Username, string Password)
{
    public string ConnectionString => $"Data Source={DataSource};{DataSourceProperties};{GetUsername()}{GetPassword()}";

    private string GetUsername()
    {
        if (string.IsNullOrWhiteSpace(Username) == false)
        {
            return $"User={Username};";
        }

        return string.Empty;
    }

    private string GetPassword()
    {
        if (string.IsNullOrWhiteSpace(Password) == false)
        {
            return $"Password={Password};";
        }

        return string.Empty;
    }
}