using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analystor.Nishomi.Api.Filters
{
    public class HandleExceptionAttribute : Attribute, IExceptionFilter
    {
        /// <summary>
        /// The message
        /// </summary>
        private readonly string _message;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleExceptionAttribute"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public HandleExceptionAttribute(string message)
        {
            this._message = message;
        }

        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        public void OnException(ExceptionContext context)
        {
            context.Exception.Data.Add("messageKey", this._message);
            throw context.Exception;
        }
    }
}
