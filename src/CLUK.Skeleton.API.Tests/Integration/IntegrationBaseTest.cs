using Dapper;

namespace CLUK.Skeleton.API.Tests.Integration;

public abstract class IntegrationBaseTest : IAsyncLifetime
{
    protected TestServerFixture TestFixture { get; private set; } = new TestServerFixture();

    protected readonly AssertionScope assertionScope = new();

    /// <summary>
    /// HTTP Get Verb Call
    /// </summary>
    /// <param name="routeUri">The route Uri, needs to be just from Controller</param>
    /// <returns>HttpResponseMessage</returns>
    protected Task<HttpResponseMessage> GetAsync(string routeUri)
    {
        return TestFixture.Client.GetAsync(routeUri.AsValidUri());
    }

    /// <summary>
    /// HTTP Post Verb Call
    /// </summary>
    /// <typeparam name="T">Type of object content parameter is</typeparam>
    /// <param name="routeUri">The route Uri, needs to be just from Controller</param>
    /// <param name="content">Content to send for Patch</param>
    /// <param name="mediaType">Media Type for content i.e. application/json</param>
    /// <returns>HttpResponseMessage</returns>
    protected Task<HttpResponseMessage> PostAsync<T>(string routeUri, T content, string mediaType = MediaTypeNames.Application.Json)
    {
        return TestFixture.Client.PostAsync(
            routeUri.AsValidUri(),
            GetContent(content, mediaType));
    }

    /// <summary>
    /// HTTP Patch Verb Call
    /// </summary>
    /// <typeparam name="T">Type of object content parameter is</typeparam>
    /// <param name="routeUri">The route Uri, needs to be just from Controller</param>
    /// <param name="content">Content to send for Patch</param>
    /// <param name="mediaType">Media Type for content i.e. application/json</param>
    /// <returns>HttpResponseMessage</returns>
    protected Task<HttpResponseMessage> PatchAsync<T>(string routeUri, T content, string mediaType = MediaTypeNames.Application.Json)
    {
        return TestFixture.Client.PatchAsync(
            routeUri.AsValidUri(),
            GetContent(content, mediaType));
    }

    private static HttpContent? GetContent<T>(T content, string mediaType)
    {
        if (content == null)
        {
            return default;
        }

        return new StringContent(JsonSerializer.Serialize(content, Constants.JsonSerializerOptions), Encoding.UTF8, MediaTypeNames.Application.Json);
    }

    public async Task InitializeAsync()
    {

        await TestFixture.sqlContainer.StartAsync();
        TestFixture.Initialize();

        await InitializeDatabase();
    }

    public async Task DisposeAsync()
    {
        TestFixture.mockRepository.VerifyAll();
        assertionScope.Dispose();

        await TestFixture.sqlContainer.StopAsync();
    }

    public async Task InsertRecords(List<ClientEntity> clientEntities)
    {
        using var connection = TestFixture.DbConnectionFactory.CreateConnection();
        connection.Open();

        await connection.ExecuteAsync(Constants.InsertSql, clientEntities[0]);

        foreach (var item in clientEntities)
        {
            await connection.ExecuteAsync(Constants.InsertClientSql, item);
        }
    }

    private async Task InitializeDatabase()
    {
        using var connection = TestFixture.DbConnectionFactory.CreateConnection();
        connection.Open();
        await connection.ExecuteAsync(Constants.DatabaseInitialisationSql);
    }
}