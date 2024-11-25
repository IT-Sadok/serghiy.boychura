namespace LinuxAgent.Models;

public class MetricsModel
{
    public double CpuUsage { get; set; }
    public double RamUsage { get; set; }
    public double StorageUsage { get; set; }
    public DateTime Timestamp { get; set; }
}