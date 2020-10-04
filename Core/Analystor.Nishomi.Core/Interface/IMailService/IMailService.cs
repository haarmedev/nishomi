namespace Analystor.Nishomi.Core
{
    using System.Threading.Tasks;

    /// <summary>
    /// The IMailService.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="mailRequest">The mail request.</param>
        /// <returns></returns>
        bool SendEmailAsync(MailRequest mailRequest);
    }
}
