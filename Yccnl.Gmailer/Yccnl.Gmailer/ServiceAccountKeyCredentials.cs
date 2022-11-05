using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Http;

namespace Yccnl.Gmailer
{
// https://developers.google.com/identity/protocols/oauth2/service-account
    public class ServiceAccountKeyCredentials : ICredentials
    {
        private readonly byte[] _keyFile;
        private readonly string _delegatedUserEmailAddress;

        public ServiceAccountKeyCredentials(byte[] keyFile, string delegatedUserEmailAddress)
        {
            _keyFile = keyFile;
            _delegatedUserEmailAddress = delegatedUserEmailAddress;
        }

        Task<IConfigurableHttpClientInitializer> ICredentials.CreateInitializer()
        {
            using var stream = new MemoryStream(_keyFile);
            IConfigurableHttpClientInitializer result = GoogleCredential.FromStream(stream)
                .CreateScoped(GmailService.Scope.GmailSend)
                .CreateWithUser(_delegatedUserEmailAddress);
            
            return Task.FromResult(result);
        }
    }
}