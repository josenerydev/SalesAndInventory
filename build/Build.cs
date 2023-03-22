using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Xunit;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.Xunit.XunitTasks;
using Nuke.Common.Tools.Docker;
using static Nuke.Common.Tools.Docker.DockerTasks;
using Serilog;
using _build;
using System.Diagnostics;

internal class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.RunMigrations);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution("SalesAndInventory.sln")] private readonly Solution Solution;

    private readonly string TestProject = "SalesAndInventory.Tests/SalesAndInventory.Tests.csproj";

    private Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });

    private Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    private Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    private Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(TestProject)
                .SetConfiguration(Configuration)
                .EnableNoBuild());
        });

    private Target StartRabbitMqContainer => _ => _
    .DependsOn(Compile)
    .Executes(() =>
    {
        var containerName = "rabbitmq-dashboard";
        var imageName = "rabbitmq:3-management";

        // Verificar se o container já existe
        var existingContainer = DockerHelper.GetContainerId(containerName);

        if (!string.IsNullOrEmpty(existingContainer))
        {
            Log.Information($"O container '{containerName}' já existe com o ID '{existingContainer}'. Removendo...");
            DockerRm(s => s
                .SetContainers(new[] { existingContainer })
                .SetForce(true));
        }

        // Executar um novo container
        DockerRun(s => s
            .SetImage(imageName)
            .SetName(containerName)
            .SetEnv(new[] { "RABBITMQ_DEFAULT_USER=user", "RABBITMQ_DEFAULT_PASS=password" })
            .SetPublish(new[] { "5672:5672", "15672:15672" })
            .SetDetach(true));

        Log.Information($"Container '{containerName}' iniciado com sucesso!");
    });

    private Target RunMigrations => _ => _
    .Executes(() =>
    {
        DotNetRun(s => s
            .SetProjectFile(Solution.GetProject("SalesAndInventory.DatabaseUpgradeTool").Path)
            .SetConfiguration(Configuration)
            .EnableNoBuild());
    });
}