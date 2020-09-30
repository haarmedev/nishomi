namespace Analystor.Nishomi.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents common API response
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        /// <param name="errors">The errors.</param>
        public ApiResponse(int status, bool success, string message, object data = null, List<Error> errors = null)
        {
            this.Status = status;
            this.Success = success;
            this.Message = message;
            this.Data = data;
            this.Errors = errors;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public object Data
        {
            get;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<Error> Errors
        {
            get;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ApiResponse"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success
        {
            get;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int Status
        {
            get;
        }

        /// <summary>
        /// Should the serialize data.
        /// </summary>
        /// <returns>
        /// true or false
        /// </returns>
        public bool ShouldSerializeData()
        {
            return this.Data != null;
        }

        /// <summary>
        /// Should the serialize errors.
        /// </summary>
        /// <returns>
        /// true or false
        /// </returns>
        public bool ShouldSerializeErrors()
        {
            return this.Errors != null && this.Errors.Count > 0;
        }
    }
}
