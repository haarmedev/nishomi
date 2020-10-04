namespace Analystor.Nishomi.Core
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The MailRequest.
    /// </summary>
    public class MailRequest
    {
        /// <summary>
        /// Gets or sets from mail.
        /// </summary>
        /// <value>
        /// From mail.
        /// </value>
        public string FromMail { get; set; }

        /// <summary>
        /// Converts to email.
        /// </summary>
        /// <value>
        /// To email.
        /// </value>
        public string ToEmail { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public List<IFormFile> Attachments { get; set; }
    }
}
