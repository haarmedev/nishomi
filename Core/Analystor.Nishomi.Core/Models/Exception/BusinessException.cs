namespace Analystor.Nishomi.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Business Exception
    /// </summary>
    public class BusinessException : ValidationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="errors">The model errors.</param>
        public BusinessException(ICollection<ValidationError> errors)
            : this(string.Empty, errors)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errors">The model errors.</param>
        public BusinessException(string message, ICollection<ValidationError> errors)
            : this(message, BusinessRuleErrorKey.BusinessError, errors)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        /// <param name="errors">The model errors.</param>
        public BusinessException(string message, BusinessRuleErrorKey errorKey, ICollection<ValidationError> errors)
            : base(message, errors)
        {
            this.ErrorKey = errorKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="errors">The model errors.</param>
        public BusinessException(string message, BusinessRuleErrorKey errorKey, Exception innerException, ICollection<ValidationError> errors)
            : base(message, innerException, errors)
        {
            this.ErrorKey = errorKey;
        }

        /// <summary>
        /// Gets the error key.
        /// </summary>
        /// <value>
        /// The error key.
        /// </value>
        public BusinessRuleErrorKey ErrorKey
        {
            get;
        }
    }
}
