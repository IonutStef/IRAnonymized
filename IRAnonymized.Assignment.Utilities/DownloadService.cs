using IRAnonymized.Assignment.Services.Interfaces;
using IRAnonymized.Assignment.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly ILogger<DownloadService> _logger;

        public DownloadService(ILogger<DownloadService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Saves a <see cref="IFormFile"/> at the specified <paramref name="folderPath"/> location.
        /// </summary>
        /// <param name="file">The file to be saved.</param>
        /// <param name="folderPath">The location at which the file should be saved.</param>
        /// <returns>A <see cref="DownloadFileResponse"/> containing the path to the file, and the download status.</returns>
        public async Task<DownloadFileResponse> SaveFile(IFormFile file, string folderPath)
        {
            var response = new DownloadFileResponse
            {
                Status = DownloadFileStatus.InvalidFile
            };

            if (file.Length > 0)
            {
                var fullPath = Path.Combine(folderPath, file.FileName);

                if (File.Exists(fullPath))
                {
                    _logger.LogInformation($"File already exists at path {fullPath}.");

                    response.Status = DownloadFileStatus.AlreadyExists;

                    return response;
                }

                _logger.LogInformation($"File {file.FileName} is being prepared to be saved at path {fullPath}.");

                using (var stream = new FileStream(fullPath, FileMode.CreateNew))
                {
                    await file.CopyToAsync(stream);
                }

                _logger.LogInformation($"File saved to {fullPath}.");

                response.Status = DownloadFileStatus.Downloaded;
                response.FilePath = fullPath;
                return response;
            }

            return response;
        }
    }
}