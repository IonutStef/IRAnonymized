using System.Threading.Tasks;

namespace IRAnonymized.Assignment.WebApi.Services.Interfaces
{
    public interface IQueueService
    {
        /// <summary>
        /// Publish a message to the queue.
        /// </summary>
        /// <typeparam name="T">Type of the message to be published.</typeparam>
        /// <param name="message">Message to be published.</param>
        /// <returns></returns>
        Task Publish<T>(T message) where T : class;
    }
}