namespace Analystor.Nishomi.Core
{
    using System.Net.Mail;

    /// <summary>
    /// MailService.
    /// </summary>
    /// <seealso cref="Analystor.Nishomi.Core.IMailService" />
    public class MailService : IMailService
    {
        //private readonly MailSettings _mailSettings;

        private readonly SmtpProvider _smtpProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailService"/> class.
        /// </summary>
        /// <param name="mailSettings">The mail settings.</param>
        public MailService(SmtpProvider smtpProvider)
        {
            //_mailSettings = mailSettings.Value;
            this._smtpProvider = smtpProvider;
        }

        public bool SendEmailAsync(MailRequest mailRequest)
        {
            var mail = new MailMessage
            {
                Body = mailRequest.Body,
                IsBodyHtml = true,
                Subject = mailRequest.Subject,
            };

            mail.From = new MailAddress("fawaskallayi@gmail.com");

            mail.To.Add(new MailAddress("fawaskallayi@gmail.com"));
            var provider = this._smtpProvider.GetCurrentSMTPClient();

            provider.Send(mail);

            return true;
        }
    }
}
