using System.Net.Mail;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Http;

namespace Yccnl.Gmailer
{
    /// <summary>
    /// Creates a new instance of the ClientIdAndClientSecret class. Use this class if you are building a desktop app
    /// or a console app. Create an OAuth 2.0 Client ID at https://console.cloud.google.com/apis and use it to
    /// construct this class.
    /// </summary>
    public sealed class ClientIdAndClientSecret : ICredentials
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _fromAddress;
        private readonly bool _forceReauthorize;

        /// <summary>
        /// Creates a new instance of the ClientIdAndClientSecret class. Use this class if you are building a desktop app
        /// or a console app. Create an OAuth 2.0 Client ID at https://console.cloud.google.com/apis and use it to
        /// construct this class.
        /// </summary>
        /// <param name="clientId">The client id you've created at https://console.cloud.google.com/apis</param>
        /// <param name="clientSecret">The client secret you've created at https://console.cloud.google.com/apis</param>
        /// <param name="fromAddress">The e-mail address which corresponds with the client_id and client_secret</param>
        /// <param name="forceReauthorize">The first time you run the application, google will save the credentials to your machine. If you provide other credentials, they will be ignored for a period of time. Unless you force Google to reauthorize.</param>
        /// <exception cref="ArgumentNullException">Throws an ArgumentNullException if either one of the parameters is null</exception>
        /// <exception cref="ArgumentException">You must provide valid values, otherwise the constructor throws an ArgumentException</exception>
        public ClientIdAndClientSecret(string clientId, string clientSecret, string fromAddress, bool forceReauthorize = false)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _fromAddress = fromAddress ?? throw new ArgumentNullException(nameof(fromAddress));
            _forceReauthorize = forceReauthorize;

            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException("The e-mail was not sent. Unable to initialize. ClientId must contain a value. " +
                                            "Go to https://console.cloud.google.com/apis, configure an OAuth 2.0 Client ID.", nameof(clientId));
            }
            
            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new ArgumentException("The e-mail was not sent. Unable to initialize. ClientSecret must contain a value. " +
                                            "Go to https://console.cloud.google.com/apis, configure an OAuth 2.0 Client ID.", nameof(clientId));
            }

            try
            {
                new MailAddress(fromAddress);
            }
            catch (Exception e)
            {
                throw new ArgumentException("The e-mail was not sent. Unable to initialize. FromAddres must contain a valid value. ", nameof(fromAddress), e);
            }
        }

        async Task<IConfigurableHttpClientInitializer> ICredentials.CreateInitializer()
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret
                },
                new[] { GmailService.Scope.GmailSend },
                _fromAddress,
                CancellationToken.None);

            if (_forceReauthorize)
            {
                await GoogleWebAuthorizationBroker.ReauthorizeAsync(credential, CancellationToken.None);
            }

            return credential;
        }
    }
}