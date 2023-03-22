using System.Diagnostics;

namespace _build
{
    public static class DockerHelper
    {
        public static string GetContainerId(string containerName)
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"ps -aqf \"name={containerName}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string containerId = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();

            return containerId;
        }
    }
}