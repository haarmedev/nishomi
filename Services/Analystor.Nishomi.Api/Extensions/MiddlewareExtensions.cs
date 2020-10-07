using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analystor.Nishomi.Api
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Uses the custom exception handler.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void UseApiExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
