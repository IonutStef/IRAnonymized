using IRAnonymized.Assignment.Services.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.Services.Interfaces
{
    public interface IDownloadService
    {
        /// <summary>
        /// Saves a <see cref="IFormFile"/> at the specified <paramref name="folderPath"/> location.
        /// </summary>
        /// <param name="file">The file to be saved.</param>
        /// <param name="folderPath">The location at which the file should be saved.</param>
        /// <returns>A <see cref="DownloadFileResponse"/> containing the path to the file, and the download status.</returns>
        Task<DownloadFileResponse> SaveFile(IFormFile file, string folderPath);
    }
}