using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using EvolveDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesAndInventory.Api.Data;
using System.Diagnostics;

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

            // Execute command inside the container
            var cmd = new[] { "/opt/mssql-tools/bin/sqlcmd", "-S", "localhost,1433", "-U", "SA", "-P", "YourStrong!Passw0rd", "-Q", "CREATE DATABASE SalesAndInventoryTest" };
            var result = _mssqlContainer.ExecAsync(cmd).Result;

            if (result.ExitCode != 0)
            {
                throw new Exception($"Error executing command: {result.Stderr}");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure())
                .Options;

            Context = new ApplicationDbContext(options);
            ApplyDatabaseMigrations(Context);
        }

        private void ApplyDatabaseMigrations(DbContext context)
        {
            var evolve = new Evolve(context.Database.GetDbConnection(), msg => Debug.WriteLine(msg))
            {
                Locations = new[] { Path.GetFullPath("..\\..\\..\\..\\db\\migrations") },
                IsEraseDisabled = true
            };

            evolve.Migrate();
        }

        public async void Dispose()
        {
            Context.Dispose();
            await _mssqlContainer.DisposeAsync();
        }
    }
}