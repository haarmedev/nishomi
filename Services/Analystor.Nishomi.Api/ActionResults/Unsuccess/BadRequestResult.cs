namespace Analystor.Nishomi.Api
{
    using Analystor.Nishomi.Core;
    using Microsoft.AspNetCore.Http;
    using System.Net;

    /// <summary>
    /// Represents Bad Request result.
    /// </summary>
    public class BadRequestResult : ErrorResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestResult" /> class.
        /// </summary>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <param name="error">The error.</param>
        /// <param name="message">The message.</param>
        public BadRequestResult(HttpResponse httpResponse, Error error, string message)
            : base(httpResponse, error, message, HttpStatusCode.BadRequest)
        {
        }
    }
}
