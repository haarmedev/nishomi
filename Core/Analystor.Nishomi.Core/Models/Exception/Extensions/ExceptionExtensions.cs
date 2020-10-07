namespace Analystor.Enitics.Core
{
    using Analystor.Nishomi.Core;
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Extensions for exception
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// The business exception status code maps
        /// </summary>
        private static Dictionary<BusinessRuleErrorKey, HttpStatusCode> _businessExceptionStatusCodeMaps = new Dictionary<BusinessRuleErrorKey, HttpStatusCode>()
        {
            { BusinessRuleErrorKey.BusinessError, HttpStatusCode.BadRequest },
            { BusinessRuleErrorKey.SizeExceed, HttpStatusCode.RequestEntityTooLarge },
            { BusinessRuleErrorKey.NotFound, HttpStatusCode.NotFound },
            { BusinessRuleErrorKey.UnAuthorized, HttpStatusCode.Unauthorized }
        };

        /// <summary>
        /// To the Enitics exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        /// EChef Exception
        /// </returns>
        public static NishomiException ToEniticsException(this Exception exception)
        {
            return exception.ToEniticsException(exception.Message);
        }

        /// <summary>
        /// To the Enitics exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// EChef Exception
        /// </returns>
        public static NishomiException ToEniticsException(this Exception exception, string message)
        {
            return new NishomiException(message, exception);
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        /// Http Status Code
        /// </returns>
        public static HttpStatusCode GetStatusCode(this BusinessException exception)
        {
            return _businessExceptionStatusCodeMaps[exception.ErrorKey];
        }
    }
}
