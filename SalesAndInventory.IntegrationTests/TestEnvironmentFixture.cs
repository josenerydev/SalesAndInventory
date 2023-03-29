using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesAndInventory.Api.Data;

namespace SalesAndInventory.IntegrationTests
{
    public class TestEnvironmentFixture : IDisposable
    {
        private readonly IContainer _mssqlContainer;
        private const ushort MssqlPort = 1433;
        public ApplicationDbContext Context { get; private set; }

        public TestEnvironmentFixture()
        {
            _mssqlContainer = new ContainerBuilder()
                .WithName("mssql")
                .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
                .WithPortBinding(MssqlPort, MssqlPort)
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SA_PASSWORD", "YourStrong!Passw0rd")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MssqlPort))
                .Build();

            _mssqlContainer.StartAsync().Wait();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure())
                .Options;

            Context = new ApplicationDbContext(options);
        }

        public async void Dispose()
        {
            Context.Dispose();
            await _mssqlContainer.DisposeAsync();
        }
    }
}