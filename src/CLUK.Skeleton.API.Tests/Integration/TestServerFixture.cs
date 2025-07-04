using CLUK.Skeleton.API.Shared.Data;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using Testcontainers.MsSql;

namespace CLUK.Skeleton.API.Tests.Integration;

public sealed class TestServerFixture : IDisposable
{
    public readonly MsSqlContainer sqlContainer;

    public readonly MockRepository mockRepository;

    public IDbConnectionFactory DbConnectionFactory { get; private set; } = default!;

    public HttpClient Client { get; private set; } = default!;

    public Mock<ILogger> LoggerMock { get; }

    public TestServerFixture()
    {
        mockRepository = new(MockBehavior.Strict);

        sqlContainer = new MsSqlBuilder().Build();

        LoggerMock = CreateMock<ILogger>();
    }

    public void Initialize()
    {
        var connectionString = sqlContainer.GetConnectionString();

        var serverName = GetValue(connectionString, "Server=");
        var databaseName = GetValue(connectionString, "Database=");
        var userName = GetValue(connectionString, "User Id=");
        var password = GetValue(connectionString, "Password=");

        var appsettings = new ApplicationSettings(new Configuration.DatabaseSettings(serverName, $"Initial Catalog={databaseName};TrustServerCertificate=True", userName, password));

        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder
                    .UseEnvironment(Microsoft.Extensions.Hosting.Environments.Development)
                    .ConfigureServices(services =>
                    {
                        services.AddScoped(_ => LoggerMock.Object);
                        services.AddScoped(_ => appsettings);
                    });
            });

        DbConnectionFactory = new DbConnectionFactory(appsettings);

        Client = application.CreateClient();
        Client.BaseAddress = new Uri("http://localhost");
    }

    public void Dispose()
    {
        Client.Dispose();
    }

    private Mock<T> CreateMock<T>() where T : class
    {
        return mockRepository.Create<T>();
    }

    private static string GetValue(string value, string name)
    {
        return value.Substring(value.IndexOf(name), value.IndexOf(";", value.IndexOf(name)) - value.IndexOf(name)).Replace(name, string.Empty);
    }
}