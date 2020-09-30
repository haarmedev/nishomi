namespace Analystor.Nishomis.Api
{
    using Analystor.Nishomi.Api;
    using Microsoft.AspNetCore.Http;
    using System.Net;

    /// <summary>
    /// Represents Created result.
    /// </summary>
    /// <typeparam name="T">Success Result</typeparam>
    public class CreatedResult<T> : SuccessResult<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedResult{T}"/> class.
        /// </summary>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <param name="location">The location.</param>
        public CreatedResult(HttpResponse httpResponse, T data, string message, string location)
            : base(httpResponse, data, message, HttpStatusCode.Created)
        {
            this.HttpResponse.Headers.Add("Location", new System.Uri(location, System.UriKind.RelativeOrAbsolute).ToString());
        }
    }
}
