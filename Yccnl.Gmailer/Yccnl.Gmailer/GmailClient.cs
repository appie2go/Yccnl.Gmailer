using System.Net.Mail;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using MimeKit;

namespace Yccnl.Gmailer;

public class GmailClient
{
    private readonly ICredentials _credentials;
    private readonly string _applicationName;

    public GmailClient(ICredentials credentials, string applicationName)
    {
        _credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
        _applicationName = applicationName ?? throw new ArgumentNullException(nameof(applicationName));
    }
    
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