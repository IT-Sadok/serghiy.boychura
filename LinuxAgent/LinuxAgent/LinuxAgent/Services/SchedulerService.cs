using LinuxAgent.Models;
using System.Timers;
using Microsoft.Extensions.Logging;

namespace LinuxAgent.Services
{
    public class SchedulerService
    {
        private readonly MetricsCollectorService _collectorService;
        private readonly DataSenderService _senderService;
        private readonly int _intervalSeconds;
        private readonly ILogger<SchedulerService> _logger;
        private System.Timers.Timer _timer;

        public SchedulerService(
            MetricsCollectorService collectorService,
            DataSenderService senderService,
            int intervalSeconds,
            ILogger<SchedulerService> logger)
        {
            _collectorService = collectorService;
            _senderService = senderService;
            _intervalSeconds = intervalSeconds;
            _logger = logger;
        }

        public void Start()
        {
            _timer = new System.Timers.Timer(_intervalSeconds * 1000);
            _timer.Elapsed += async (sender, e) => await CollectAndSendMetricsAsync();
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _logger.LogInformation("Scheduler started.");
        }

        private async Task CollectAndSendMetricsAsync()
        {
            var metrics = _collectorService.CollectMetrics();
            _logger.LogInformation($"Collected Metrics: CPU={metrics.CpuUsage}%, RAM={metrics.RamUsage}%, Storage={metrics.StorageUsage}%");
            
            bool success = await _senderService.SendMetricsAsync(metrics);
            if (success)
            {
                _logger.LogInformation("Metrics sent successfully.");
            }
            else
            {
                _logger.LogWarning("Failed to send metrics.");
            }
        }

        public void Stop()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _logger.LogInformation("Scheduler stopped.");
        }
    }
}