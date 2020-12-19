namespace Analystor.Nishomi.Core
{
    using System;
    using System.Net;
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

        public bool SendAckMail(MailRequest mailRequest)
        {
            var mail = new MailMessage
            {
                Body = mailRequest.Body,
                IsBodyHtml = true,
                Subject = mailRequest.Subject,
            };

            mail.From = new MailAddress(mailRequest.FromMail);

            mail.To.Add(new MailAddress(mailRequest.ToEmail));
            var provider = this._smtpProvider.GetCurrentSMTPClient();

            provider.Send(mail);
            return true;
        }

        public bool SendEmailAsync(MailRequest mailRequest)
        {
            

            string serverName = "smtp.live.com";
            int port = 587;
            string fromEmail = mailRequest.FromMail;
            string password = "Nishomi2020#";
            string displayName = "Nishomi Abayas";
            string toEmail = mailRequest.ToEmail;
            //string subject = "You have a new product request from Prashobh";
            //string messageBody = "Hello Nishomi Abayas, Prashobh is showing interest to your product #00449.";

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var fromAddress = new MailAddress(fromEmail, displayName);
            var toAddress = new MailAddress(toEmail);
            string fromPassword = password;
            var smtp = new SmtpClient
            {
                Host = serverName,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),

            };

            using (var mailmessage = new MailMessage(fromAddress, toAddress)
            {
                IsBodyHtml = true,
                Subject = mailRequest.Subject,
                Body = mailRequest.Body
            })
            {
                try
                {
                    smtp.Send(mailmessage);
                    return true;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
