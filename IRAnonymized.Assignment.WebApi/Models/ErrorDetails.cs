using Newtonsoft.Json;

namespace IRAnonymized.Assignment.WebApi.Models
{
    /// <summary>
    /// Error model to be displayed for Internal Server Error.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Status code of the exception.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Message of the exception.
        /// </summary>
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}