using LinuxAgent.Models;
using System.Diagnostics;

namespace LinuxAgent.Services
{
    public class MetricsCollectorService
    {
        public MetricsModel CollectMetrics()
        {
            return new MetricsModel
            {
                CpuUsage = GetCpuUsage(),
                RamUsage = GetRamUsage(),
                StorageUsage = GetStorageUsage(),
                Timestamp = DateTime.UtcNow
            };
        }

        private double GetCpuUsage()
        {
            return ExecuteCommand("top -bn1 | grep 'Cpu(s)' | awk '{print $2}'");
        }

        private double GetRamUsage()
        {
            return ExecuteCommand("free | grep Mem | awk '{print $3/$2 * 100.0}'");
        }

        private double GetStorageUsage()
        {
            return ExecuteCommand("df / | tail -1 | awk '{print $5}' | sed 's/%//g'");
        }

        private double ExecuteCommand(string command)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{command}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                var result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (double.TryParse(result.Trim(), out double value))
                {
                    return value;
                }
            }
            catch
            {
                // Log error
            }

            return -1;
        }
    }
}