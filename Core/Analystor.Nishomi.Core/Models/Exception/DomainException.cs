namespace Analystor.Nishomi.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Domain Exception
    /// </summary>
    public class DomainException : ValidationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="errors">The model errors.</param>
        public DomainException(ICollection<ValidationError> errors)
            : base(errors)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errors">The model errors.</param>
        public DomainException(string message, ICollection<ValidationError> errors)
            : base(message, errors)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="errors">The model errors.</param>
        public DomainException(string message, Exception innerException, ICollection<ValidationError> errors)
            : base(message, innerException, errors)
        {
        }
    }
}
