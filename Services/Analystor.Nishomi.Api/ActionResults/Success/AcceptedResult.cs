namespace Analystor.Nishomi.Api
{
    using Microsoft.AspNetCore.Http;
    using System.Net;

    /// <summary>
    /// Represents Accepted result.
    /// </summary>
    /// <typeparam name="T">Success Result</typeparam>
    public class AcceptedResult<T> : SuccessResult<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptedResult{T}"/> class.
        /// </summary>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        public AcceptedResult(HttpResponse httpResponse, T data, string message)
            : base(httpResponse, data, message, HttpStatusCode.Accepted)
        {
        }
    }
}
