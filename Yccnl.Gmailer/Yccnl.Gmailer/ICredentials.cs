using Google.Apis.Http;

namespace Yccnl.Gmailer
{
    public interface ICredentials
    {
        internal Task<IConfigurableHttpClientInitializer> CreateInitializer();
    }
}