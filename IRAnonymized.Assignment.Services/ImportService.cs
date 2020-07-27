using IRAnonymized.Assignment.Common.QueueContracts;
using IRAnonymized.Assignment.Services.Interfaces;
using IRAnonymized.Assignment.WebApi.Services.Settings;
using IRAnonymized.Assignment.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using IRAnonymized.Assignment.Common.QueueContracts.Request;
using IRAnonymized.Assignment.Services.Models;
using IRAnonymized.Assignment.WebApi.Services.Models.Response;

namespace IRAnonymized.Assignment.WebApi.Services
{
    public class ImportService : IImportService
    {
        private readonly ILogger<ImportService> _logger;
        private readonly IDownloadService _downloadService;
        private readonly IQueueService _queueService;
        private readonly IFileImportService _fileImportService;
        private readonly AppSettings _options;

        public ImportService(ILogger<ImportService> logger, IDownloadService downloadService, 
            IFileImportService fileImportService, IOptions<AppSettings> options, IQueueService queueService)
        {
            _logger = logger;
            _downloadService = downloadService;
            _fileImportService = fileImportService;
            _queueService = queueService;
            _options = options.Value;
        }

        public async Task<ImportFileResponse> ImportDataFromFileAsync(IFormFile file)
        {
            var response = new ImportFileResponse();

            var downloadResponse = await _downloadService.SaveFile(file, _options.LocalStorageSourceFolderPath);

            if(downloadResponse.Status == DownloadFileStatus.Downloaded)
            {
                await _queueService.Publish<ImportFileEvent>(new ImportFileEventRequest
                {
                    FileLocalPath = downloadResponse.FilePath
                });

                response.Status = ImportFileStatus.SentToBeProcessed;
                return response;
            }

            response.Status = EnumConversion(downloadResponse.Status);
            return response;
        }

        /// <summary>
        /// Convert a <see cref="DownloadFileStatus"/> to <see cref="ImportFileStatus"/>.
        /// </summary>
        /// <param name="status"><see cref="DownloadFileStatus"/> to be converted.</param>
        /// <returns>Converted <see cref="ImportFileStatus"/>.</returns>
        private ImportFileStatus EnumConversion(DownloadFileStatus status)
        {
            switch(status)
            {
                case DownloadFileStatus.AlreadyExists:
                    return ImportFileStatus.AlreadyExists;
                case DownloadFileStatus.InvalidFile:
                    return ImportFileStatus.InvalidFile;
                case DownloadFileStatus.Downloaded:
                    return ImportFileStatus.SentToBeProcessed;
                default:
                    return ImportFileStatus.InvalidFile;
            }
        }
    }
}