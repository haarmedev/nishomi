namespace Analystor.Nishomi.Api.Controllers
{
    using Analystor.Nishomi.Core;
    using Analystor.Nishomis.Api;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// ApiController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        //protected readonly ILogger<ApiController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        //public ApiController(ILogger<ApiController> logger)
        //{
        //    this._logger = logger;
        //}

        /// <summary>
        /// Oks the specified data.
        /// </summary>
        /// <typeparam name="T">The Entity.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// Ok Result
        /// </returns>
        protected OkResult<T> Ok<T>(T data, string message)
            where T : class
        {
            return new OkResult<T>(this.Response, data, message);
        }

        /// <summary>
        /// Createds the specified data.
        /// </summary>
        /// <typeparam name="T">The Entity.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <param name="location">The location.</param>
        /// <returns>
        /// Created Result
        /// </returns>
        protected CreatedResult<T> Created<T>(T data, string message, string location)
            where T : class
        {
            return new CreatedResult<T>(this.Response, data, message, location);
        }

        /// <summary>
        /// Accepteds the specified data.
        /// </summary>
        /// <typeparam name="T">The Entity.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// Accepted Result
        /// </returns>
        protected AcceptedResult<T> Accepted<T>(T data, string message)
            where T : class
        {
            return new AcceptedResult<T>(this.Response, data, message);
        }

        /// <summary>
        /// Bads the request.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// Bad Request Result
        /// </returns>
        protected Api.BadRequestResult BadRequest(Error error, string message)
        {
            return new Api.BadRequestResult(this.Response, error, message);
        }

        /// <summary>
        /// Gones the specified error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// Gone Result
        /// </returns>
        protected GoneResult Gone(Error error, string message)
        {
            return new GoneResult(this.Response, error, message);
        }

        /// <summary>
        /// Nots the found.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// Not Found Result
        /// </returns>
        protected Api.NotFoundResult NotFound(Error error, string message)
        {
            return new Api.NotFoundResult(this.Response, error, message);
        }
    }
}