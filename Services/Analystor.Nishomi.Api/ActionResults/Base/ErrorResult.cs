namespace Analystor.Nishomi.Api
{
    using Analystor.Nishomi.Api;
    using Analystor.Nishomi.Core;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Represents error result
    /// </summary>
    public class ErrorResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult" /> class.
        /// </summary>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <param name="error">The error.</param>
        /// <param name="message">The message.</param>
        /// <param name="status">The status.</param>
        public ErrorResult(HttpResponse httpResponse, Error error, string message, HttpStatusCode status)
            : base(httpResponse, message, status)
        {
            this.Error = error;

            if (this.Error != null)
            {
                this.Error.Reason = this.Status.ToString();
                this.Error.ErrorCode = ((int)this.Status).ToString();
            }
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        protected Error Error
        {
            get;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// The Value
        /// </returns>
        protected override object GetValue()
        {
            return new ErrorResponse
            {
                Message = this.Message,
                Errors = new List<Error>
                {
                    this.Error
                }
            };
        }
    }
}
