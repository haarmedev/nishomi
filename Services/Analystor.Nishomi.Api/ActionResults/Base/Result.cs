namespace Analystor.Nishomi.Api
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents base class for Result.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.IActionResult" />
    public abstract class Result : IActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <param name="message">The message.</param>
        /// <param name="status">The status.</param>
        public Result(HttpResponse httpResponse, string message, HttpStatusCode status)
        {
            this.HttpResponse = httpResponse;
            this.Message = message;
            this.Status = status;
        }

        /// <summary>
        /// Gets the HTTP response.
        /// </summary>
        /// <value>
        /// The HTTP response.
        /// </value>
        public HttpResponse HttpResponse
        {
            get;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        protected string Message
        {
            get;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        protected HttpStatusCode Status
        {
            get;
        }

        /// <summary>
        /// Executes the result operation of the action method asynchronously. This method is called by MVC to process
        /// the result of an action method.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes
        /// information about the action that was executed and request information.</param>
        /// <returns>
        /// A task that represents the asynchronous execute operation.
        /// </returns>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this.GetValue())
            {
                StatusCode = (int)this.Status,
            };

            await objectResult.ExecuteResultAsync(context);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>
        /// The Value
        /// </returns>
        protected abstract object GetValue();
    }
}
