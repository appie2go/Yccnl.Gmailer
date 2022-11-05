using System.Net.Mail;

namespace Yccnl.Gmailer;

public interface IGmailClient
{
    Task Send(MailMessage mailMessage);
}