using System;

namespace IRAnonymized.Assignment.Common.QueueContracts
{
    /// <summary>
    /// Event request for Import File.
    /// </summary>
    public interface ImportFileEvent
    {
        /// <summary>
        /// Unique Identifier required for messages sent through Mass Transit.
        /// </summary>
        Guid CorrelationId { get; }

        /// <summary>
        /// Local Path to which the file was saved.
        /// </summary>
        string FileLocalPath { get; }
    }
}