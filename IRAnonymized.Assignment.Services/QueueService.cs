using IRAnonymized.Assignment.WebApi.Services.Interfaces;
using MassTransit;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.WebApi.Services
{
    public class QueueService : IQueueService
    {
        IPublishEndpoint _publishEndpoint;

        public QueueService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Publish a message to the queue.
        /// </summary>
        /// <typeparam name="T">Type of the message to be published.</typeparam>
        /// <param name="message">Message to be published.</param>
        /// <returns></returns>
        public async Task Publish<T>(T message)
            where T : class
        {
            await _publishEndpoint.Publish<T>(message);
        }
    }
}