using IRAnonymized.Assignment.Common.QueueContracts;
using IRAnonymized.Assignment.Services.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.Consummers.Consummers
{
    /// <summary>
    /// Consummer for the <see cref="ImportFileEvent"/> queue.
    /// </summary>
    public class ImportFileConsummer : IConsumer<ImportFileEvent>
    {
        IFileImportService _fileImportService;
        ILogger _logger;

        public ImportFileConsummer(IFileImportService fileImportService, ILogger<ImportFileConsummer> logger)
        {
            _fileImportService = fileImportService;
            _logger = logger;
        }

        /// <summary>
        /// Consumes a <see cref="ImportFileEvent"/>.
        /// </summary>
        /// <param name="context">MassTransit context.</param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<ImportFileEvent> context)
        {
            var message = context.Message;

            _logger.LogInformation($"Received message with Id: {message.CorrelationId}.");

            if(!File.Exists(message.FileLocalPath))
            {
                _logger.LogError($"Required file does not exist at location: {message.FileLocalPath}.");
                return;
            }

            var linesImported = await _fileImportService.Import(message.FileLocalPath);

            File.Delete(message.FileLocalPath);

            _logger.LogInformation($"Processed message with Id: {message.CorrelationId}. \n" +
                $"{linesImported} entries were added.");
        }
    }
}