using System;

namespace IRAnonymized.Assignment.Common.QueueContracts.Request
{
    /// <summary>
    /// Event request for Import File.
    /// </summary>
    public class ImportFileEventRequest : ImportFileEvent
    {
        /// <summary>
        /// Unique Identifier required for messages sent through Mass Transit.
        /// </summary>
        public Guid CorrelationId => Guid.NewGuid();

        /// <summary>
        /// Local Path to which the file was saved.
        /// </summary>
        public string FileLocalPath { get; set; }
    }
}