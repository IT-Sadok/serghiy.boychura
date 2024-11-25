using LinuxAgent.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace LinuxAgent.Services
{
    public class DataSenderService
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl;
        private readonly ILogger<DataSenderService> _logger;

        public DataSenderService(string serverUrl, ILogger<DataSenderService> logger)
        {
            _httpClient = new HttpClient();
            _serverUrl = serverUrl;
            _logger = logger;
        }

        public async Task<bool> SendMetricsAsync(MetricsModel metrics)
        {
            try
            {
                var json = JsonSerializer.Serialize(metrics);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_serverUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Metrics sent successfully.");
                    return true;
                }
                else
                {
                    _logger.LogWarning($"Failed to send metrics. Server responded with: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending metrics: {ex.Message}");
            }

            return false;
        }
    }
}