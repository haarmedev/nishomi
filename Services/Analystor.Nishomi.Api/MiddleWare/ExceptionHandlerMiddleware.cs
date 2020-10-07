using Analystor.Enitics.Core;
using Analystor.Nishomi.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Analystor.Nishomi.Api
{
    public class ExceptionHandlerMiddleware
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// The error response
        /// </summary>
        private ErrorResponse _errorResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="logger">The logger.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this._next = next;
            this._errorResponse = new ErrorResponse();
            this._logger = logger;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The Task
        /// </returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception exception)
            {
                await this.HandleException(context, exception);
            }
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>
        /// The Task
        /// </returns>
        private Task HandleException(HttpContext context, Exception exception)
        {
            var exceptionMessage = $"{exception.Message}";
            var innerExceptionMessage = $"{exception.InnerException?.ToString()}";

            var combinedMessage = $"{exceptionMessage}__:__{innerExceptionMessage}";

            this._logger.LogError(exception, combinedMessage);

            this._errorResponse = new ErrorResponse();

            if (exception != null)
            {
                var EniticsException = exception.ToEniticsException();

                Type exceptionType = exception.GetType();

                HttpStatusCode status = HttpStatusCode.InternalServerError;

                if (exceptionType.IsSubclassOf(typeof(ValidationException)) || exceptionType == typeof(ValidationException))
                {
                    var validationErrors = ((ValidationException)exception).Errors;
                    foreach (var item in validationErrors)
                    {
                        this._errorResponse.Errors.Add(new Error()
                        {
                            Message = item.Message
                        });
                    }

                    if (exception is PropertyException)
                    {
                        foreach (var item in this._errorResponse.Errors)
                        {
                            item.ErrorCode = EniticsException.ExceptionId.ToString();
                            item.Reason = "Property Validation Failed";
                        }

                        status = HttpStatusCode.BadRequest;
                    }
                    else if (exception is DomainException)
                    {
                        foreach (var item in this._errorResponse.Errors)
                        {
                            item.ErrorCode = EniticsException.ExceptionId.ToString();
                            item.Reason = "Domain Validation Failed";
                        }

                        status = HttpStatusCode.BadRequest;
                    }
                    else if (exception is BusinessException)
                    {
                        foreach (var item in this._errorResponse.Errors)
                        {
                            item.ErrorCode = EniticsException.ExceptionId.ToString();
                            item.Reason = "Business Rule Violated";
                        }

                        status = ((BusinessException)exception).GetStatusCode();
                    }
                }
                else
                {
                    this._errorResponse.Errors.Add(new Error()
                    {
                        ErrorCode = EniticsException.ExceptionId.ToString(),
                        Reason = "Unhandled Exception",
                        Message = exception.InnerException == null ? exception.Message : exception.InnerException.ToString()
                    });

                    status = HttpStatusCode.InternalServerError;
                }

                if (exception != null && exception.Data != null)
                {
                    this._errorResponse.Message = exception.Data["messageKey"] == null ? "Unhandled Exception Occured" : exception.Data["messageKey"].ToString();
                }

                context.Response.StatusCode = (int)status;
            }

            context.Response.ContentType = "application/json; charset=utf-8";

            var result = JsonConvert.SerializeObject(this._errorResponse);

            return context.Response.WriteAsync(result);
        }
    }
}
