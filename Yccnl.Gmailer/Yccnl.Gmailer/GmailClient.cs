using System.Net.Mail;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using MimeKit;

namespace Yccnl.Gmailer
{
    /// <summary>
    /// The GmailClient class is a proxy to the Gmail API. It supports sending e-mails only. Initialize it with
    /// an instance of the ICredentials interface (ServiceAccountKeyCredentials class or the ClientIdAndClientSecret class).
    /// </summary>
    public class GmailClient : IGmailClient
    {
        private readonly ICredentials _credentials;
        private readonly string _applicationName;

        /// <summary>
        /// The GmailClient class is a proxy to the Gmail API. It supports sending e-mails only. Initialize it with
        /// an instance of the ICredentials interface (ServiceAccountKeyCredentials class or the ClientIdAndClientSecret class).
        /// </summary>
        /// <param name="credentials">An instance of ServiceAccountKeyCredentials class or the ClientIdAndClientSecret class</param>
        /// <param name="applicationName">The name of the application.</param>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if either one of the parameters is null</exception>
        /// <exception cref="ArgumentException">Throws an ArgumentException if an invalid applicationName has been submitted.</exception>
        public GmailClient(ICredentials credentials, string applicationName)
        {
            _credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            _applicationName = applicationName ?? throw new ArgumentNullException(nameof(applicationName));

            if (string.IsNullOrEmpty(applicationName))
            {
                throw new ArgumentException("The e-mail was not sent. Unable to initialize. ApplicationName must contain a value. ", 
                    nameof(applicationName));
            }
        }

        /// <summary>
        /// Collects an access token and sends the e-mail
        /// </summary>
        /// <param name="mailMessage">An instance of System.Net.Mail with the e-mail you want to send.</param>
        public async Task Send(MailMessage mailMessage)
        {
            using var mimeMessage = MimeMessage.CreateFromMailMessage(mailMessage);

            var gmailMessage = new Google.Apis.Gmail.v1.Data.Message
            {
                Raw = Encode(mimeMessage)
            };

            using var service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = await _credentials.CreateInitializer(),
                ApplicationName = _applicationName,
            });

            var request = service.Users.Messages.Send(gmailMessage, "me");

            await request.ExecuteAsync();
        }

        private static string Encode(MimeMessage mimeMessage)
        {
            using var ms = new MemoryStream();

            mimeMessage.WriteTo(ms);
            return Convert.ToBase64String(ms.GetBuffer())
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }
    }
}