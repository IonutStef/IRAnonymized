using System.IO;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.Services.Interfaces
{
    public interface IFileImportService
    {
        /// <summary>
        /// Import a file from a specified <paramref name="path"/> location.
        /// </summary>
        /// <param name="path">Location at which the file exists.</param>
        /// <returns>Number of entries found in the file.</returns>
        Task<int> Import(string path);
    }
}