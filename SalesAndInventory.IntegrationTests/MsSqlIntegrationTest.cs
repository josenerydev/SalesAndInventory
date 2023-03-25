using DotNet.Testcontainers.Builders;

using DotNet.Testcontainers.Builders;

using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using DotNet.Testcontainers.Containers;

namespace SalesAndInventory.IntegrationTests
{
    public class MsSqlIntegrationTest : IDisposable
    {
        private readonly TestcontainerDatabase _container;

        public MsSqlIntegrationTest()
        {
            _container = new TestcontainersBuilder<MsSqlTestcontainer>()
        .WithDatabase(new MsSqlTestcontainerConfiguration
        {
            Password = "localdevpassword#123",
        })
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithCleanUp(true)
        .Build();
        }

        [Fact]
        public void TestDatabaseConnection()
        {
            using var connection = new SqlConnection(_container.GetConnectionString());
            connection.Open();

            using var command = new SqlCommand("SELECT 1", connection);
            var result = command.ExecuteScalar();

            Assert.Equal(1, result);
        }

        public void Dispose()
        {
            _container.StopAsync().GetAwaiter().GetResult();
        }
    }
}