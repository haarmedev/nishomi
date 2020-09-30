namespace Analystor.Nishomi.Api
{
    using Analystor.Nishomi.Core;
    using Microsoft.AspNetCore.Http;
    using System.Net;

    /// <summary>
    /// Represents Not Found result.
    /// </summary>
    public class NotFoundResult : ErrorResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundResult"/> class.
        /// </summary>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <param name="error">The error.</param>
        /// <param name="message">The message.</param>
        public NotFoundResult(HttpResponse httpResponse, Error error, string message)
            : base(httpResponse, error, message, HttpStatusCode.NotFound)
        {
        }
    }
}
