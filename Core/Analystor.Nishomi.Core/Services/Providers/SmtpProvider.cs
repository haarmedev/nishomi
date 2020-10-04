namespace Analystor.Nishomi.Core
{
    using System;
    using System.Net.Mail;

    /// <summary>
    /// SMTP Provider
    /// </summary>
    public class SmtpProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpProvider"/> class.
        /// </summary>
        public SmtpProvider()
        {
        }

        /// <summary>
        /// Gets or sets the SMTP resolver.
        /// </summary>
        /// <value>
        /// The SMTP resolver.
        /// </value>
        public Func<SmtpClient> SMTPResolver
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current SMTP client.
        /// </summary>
        /// <returns>
        /// SMTP Client
        /// </returns>
        public SmtpClient GetCurrentSMTPClient()
        {
            return this.SMTPResolver();
        }
    }
}
