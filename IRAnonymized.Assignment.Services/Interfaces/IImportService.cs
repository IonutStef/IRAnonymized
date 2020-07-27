using IRAnonymized.Assignment.WebApi.Services.Models.Response;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.WebApi.Services.Interfaces
{
    public interface IImportService
    {
        /// <summary>
        /// Import data from file.
        /// </summary>
        /// <param name="file">file that stores the data to be imported.</param>
        /// <returns>Response containing a status of the import.</returns>
        Task<ImportFileResponse> ImportDataFromFileAsync(IFormFile file);
    }
}