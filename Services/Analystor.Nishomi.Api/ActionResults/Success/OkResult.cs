namespace Analystor.Nishomi.Api
{
    using Microsoft.AspNetCore.Http;
    using System.Net;

    /// <summary>
    /// Represents Ok result.
    /// </summary>
    /// <typeparam name="T">Success Result</typeparam>
    public class OkResult<T> : SuccessResult<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OkResult{T}"/> class.
        /// </summary>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        public OkResult(HttpResponse httpResponse, T data, string message)
            : base(httpResponse, data, message, HttpStatusCode.OK)
        {
        }
    }
}
