using IRAnonymized.Assignment.Services.Interfaces;
using IRAnonymized.Assignment.WebApi.Services.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.WebApi.Infrastructure
{
    /// <summary>
    /// Hosted service to import a file from the disk at startup.
    /// </summary>
    public class DataInitializerHostedService : BackgroundService
    {
        private readonly AppSettings _options;
        private readonly IFileImportService _fileImportService;
        private readonly ILogger<DataInitializerHostedService> _logger;

        public DataInitializerHostedService(IOptions<AppSettings> options, 
            IFileImportService fileImportService, ILogger<DataInitializerHostedService> logger)
        {
            _options = options.Value;
            _fileImportService = fileImportService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Initializing data from: {_options.LocalStorageSourceFolderPath}");

            var itemsImported = await _fileImportService.Import(_options.LocalStorageSourceFolderPath);

            _logger.LogInformation($"Stored {itemsImported} items.");
        }
    }
}