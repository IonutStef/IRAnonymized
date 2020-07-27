namespace IRAnonymized.Assignment.Services.Models
{
    /// <summary>
    /// Response for a Download action.
    /// </summary>
    public class DownloadFileResponse
    {
        /// <summary>
        /// Path to the downloaded file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Status of the download process.
        /// </summary>
        public DownloadFileStatus Status { get; set; }
    }
}