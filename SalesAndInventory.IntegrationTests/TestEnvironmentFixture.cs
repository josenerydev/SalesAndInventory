using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesAndInventory.Api.Data;
using System.Diagnostics;

namespace SalesAndInventory.IntegrationTests
{
    public class TestEnvironmentFixture : IDisposable
    {
        private static TestEnvironmentFixture _instance;

        public static TestEnvironmentFixture Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TestEnvironmentFixture();
                }

                return _instance;
            }
        }

        public ApplicationDbContext Context { get; private set; }

        public TestEnvironmentFixture()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure())
                .Options;

            Context = new ApplicationDbContext(options);

            InitializeAsync().GetAwaiter().GetResult();
        }

        public async Task InitializeAsync()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var targetDirectory = @"..\..\..\..\mssql";
            var basePath = Path.GetRelativePath(currentDirectory, Path.Combine(currentDirectory, targetDirectory));
            var dockerComposeFile = Path.Combine(basePath, "docker-compose.yml");

            // Stop any existing containers with the same name
            var stopCommand = $"-f \"{dockerComposeFile}\" down";
            await RunDockerComposeCommand(stopCommand);

            // Remove container
            var removeCommand = $"rm -f mssql-mssql-1";
            await RunDockerCommand(removeCommand);

            // Start the Docker Compose services
            var upCommand = $"-f \"{dockerComposeFile}\" up -d";
            await RunDockerComposeCommand(upCommand);

            await WaitForDatabaseAvailabilityAsync();
        }

        private async Task RunDockerCommand(string command)
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"Docker command failed with exit code {process.ExitCode}. Output: {output}, Error: {error}");
            }
        }

        private async Task RunDockerComposeCommand(string command)
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "docker-compose",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"Docker Compose failed with exit code {process.ExitCode}. Output: {output}, Error: {error}");
            }
        }

        public async Task DisposeAsync()
        {
            // Stop and remove Docker Compose services
            var currentDirectory = Directory.GetCurrentDirectory();
            var targetDirectory = @"..\..\..\..\mssql";
            var basePath = Path.GetRelativePath(currentDirectory, Path.Combine(currentDirectory, targetDirectory));
            var dockerComposeFile = Path.Combine(basePath, "docker-compose.yml");

            var command = $"-f \"{dockerComposeFile}\" down";

            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "docker-compose",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"Docker Compose failed with exit code {process.ExitCode}. Output: {output}, Error: {error}");
            }
        }

        private async Task WaitForDatabaseAvailabilityAsync()
        {
            var connection = new SqlConnection(Context.Database.GetDbConnection().ConnectionString);
            var retryCount = 0;
            var maxRetries = 60;
            var delayBetweenRetries = TimeSpan.FromSeconds(2);

            while (retryCount < maxRetries)
            {
                try
                {
                    await connection.OpenAsync();
                    connection.Close();
                    break;
                }
                catch
                {
                    retryCount++;
                    if (retryCount == maxRetries)
                    {
                        throw new TimeoutException("Database is not available after waiting for a long time.");
                    }

                    await Task.Delay(delayBetweenRetries);
                }
            }
        }

        public void Dispose()
        {
            DisposeAsync().GetAwaiter().GetResult();
        }
    }
}