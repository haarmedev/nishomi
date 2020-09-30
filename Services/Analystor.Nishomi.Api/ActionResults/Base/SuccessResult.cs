namespace Analystor.Nishomi.Api
{
    using Analystor.Nishomi.Core;
    using Microsoft.AspNetCore.Http;
    using System.Net;

    /// <summary>
    /// Represents success result
    /// </summary>
    /// <typeparam name="T">The Result</typeparam>
    public class SuccessResult<T> : Result
        where T : class
    {
        /// <summary>
        /// The data
        /// </summary>
        private readonly T _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessResult{T}"/> class.
        /// </summary>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <param name="status">The status.</param>
        public SuccessResult(HttpResponse httpResponse, T data, string message, HttpStatusCode status)
            : base(httpResponse, message, status)
        {
            this._data = data;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// The Value
        /// </returns>
        protected override object GetValue()
        {
            return new SuccessResponse<T>
            {
                Data = this._data,
                Message = this.Message
            };
        }
    }
}
