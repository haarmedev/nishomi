namespace Analystor.Nishomi.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents Error Response
    /// </summary>
    public class ErrorResponse : ReponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
        /// </summary>
        public ErrorResponse()
        {
            this.Errors = new List<Error>();
        }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<Error> Errors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ErrorResponse"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success => false;
    }
}
