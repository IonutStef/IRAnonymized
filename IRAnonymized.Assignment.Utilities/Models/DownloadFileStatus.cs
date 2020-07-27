using System;
using System.Collections.Generic;
using System.Text;

namespace IRAnonymized.Assignment.Services.Models
{
    /// <summary>
    /// Status of the download process.
    /// </summary>
    public enum DownloadFileStatus
    {
        InvalidFile,
        AlreadyExists,
        Downloaded
    }
}
