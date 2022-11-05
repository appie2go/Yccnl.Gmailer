using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Http;

namespace Yccnl.Gmailer;

public sealed class ClientIdAndClientSecret : ICredentials
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _fromAddress;

    public ClientIdAndClientSecret(string clientId, string clientSecret, string fromAddress)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
        _fromAddress = fromAddress;
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

        return credential;
    }
}