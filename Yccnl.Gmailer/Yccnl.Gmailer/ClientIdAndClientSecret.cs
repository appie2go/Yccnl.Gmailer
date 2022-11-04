using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;

namespace Yccnl.Gmailer;

public sealed class ClientIdAndClientSecret : ICredentials
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _fromEmailAddress;

    public ClientIdAndClientSecret(string clientId, string clientSecret, string fromEmailAddress)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
        _fromEmailAddress = fromEmailAddress;
    }
    
    async Task<IConfigurableHttpClientInitializer> ICredentials.CreateInitializer()
    {
        var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret
            },
            new[] { "https://www.googleapis.com/auth/gmail.send" },
            _fromEmailAddress,
            CancellationToken.None);

        return credential;
    }
}