# Yccnl.Gmailer

Since may 2022, Gmail no longer supports sending e-mails programatically using the SMTP protocol. 

## Alternative to sending mails with SMTP

Instead you can send e-mails using the Google API. But i.m.o. this API is a pain. So, this repository is a wrapper around it. You can either grab code from it and customize it to your needs, or use this code directly by installing the [NuGet Package](https://www.nuget.org/packages/Yccnl.Gmail) that comes with this repo:


```ps
dotnet add package Yccnl.Gmail --version 0.0.4
```

## How to use it in a console or a desktop app (OAuth 2.0 Client Id)
To use this library, create a OAuth 2.0 Client id in the Google Cloud Console. This is where you create the client_id and the secret you'll need in the code. Then, download the Yccnl.Gmail NuGet package in your project and create a new instance of the GailClient class.

Note: Use using the OAuth 2.0 Client Id does not require a Google workspace. As a result, Google will prompt the end-user to authenticate once, the first time you start the application. This is problematic for web apps or stand-alone processes. In that case, use the service account approach.

### Configuring Google/gMail:

* Go to [https://console.cloud.google.com/apis](https://console.cloud.google.com/apis)
* Click `Credentials`, and click `Create credentials`
* Create an `OAuth Client Id` for a `Desktop App` and copy the client_id and the client_secret.

### The C# code:
If you're using C#, be sure to add the appropriate 'using':

```C#
using Yccnl.Gmailer;
```

Use the following code to send an e-mail. The library can send 'normal' system.net.mail objects.

```c#
var message = new System.Net.Mail.MailMessage();
message.To.Add("recipient@domain.com");
message.Subject = "Test";
message.Body = "It works!!";

var credentials = new ClientIdAndClientSecret("[insert-client-id-here]", "[insert-client-secret-here]", "you@gmail.com");
var client = new GmailClient(credentials, "MyApp");
await client.Send(message);
```

### The VB.NET code:

If you're using VB, be sure to add the appropriate 'imports':
```VB
Imports Yccnl.Gmailer
```

Use the following code to send an e-mail. The library can send 'normal' system.net.mail objects.

```VB
Dim message = new System.Net.Mail.MailMessage()
message.To.Add("recipient@domain.com")
message.Subject = "Test"
message.Body = "It works!!"

Dim credentials = new ClientIdAndClientSecret("[insert-client-id-here]", "[insert-client-secret-here]", "you@gmail.com")
Dim client = new GmailClient(credentials, "MyApp")
client.Send(message).Wait()
```
### Forcing reauthentication
It is likely that you'll fiddle around with client_ids and client_secrets in the Google Cloud Console. But once you've succesfully started your application, these credentials are pretty 'sticky'. So when you think you are using a new client_id and client_secret (because that's in the code), Google might still use the previous client_id and client_secret.

When you renew client_ids and client_secrets, be sure to enforce reauthentication. Like so:

```C#
var credentials = new ClientIdAndClientSecret("[insert-client-id-here]", "[insert-client-secret-here]", "you@gmail.com");
credentials.Renew();
```
or:
```VB
Dim credentials = new ClientIdAndClientSecret("[insert-client-id-here]", "[insert-client-secret-here]", "you@gmail.com")
credentials.Renew()
```


## Server-to-server use cases (Service Account)

As mentioned earlier, the method above is unfit for server-to-server web apps. When you have a server-to-server, or a stand-alone/daemon-like use-case, go to [https://console.cloud.google.com/apis](https://console.cloud.google.com/apis) and create a service account. Create a key, and download the corresponding p12 certificate or a json key. Be sure to enable domain wide delegation. How to do this exactly has been described in this article:

[https://developers.google.com/identity/protocols/oauth2/service-account#delegatingauthority](https://developers.google.com/identity/protocols/oauth2/service-account#delegatingauthority)

Note that in this case you need a Google Workspace subscription. Sending e-mails from the server side with this library is done as follows:

Include the appropriate using:
```C#
using Yccnl.Gmailer;
```

And send a mail with the key:
```C#
var message = new System.Net.Mail.MailMessage();
message.To.Add("recipient@domain.com");
message.Subject = "Test";
message.Body = "It works!!";

var keyFile = System.IO.File.ReadAllBytes("key.json"); 
var credentials = new ServiceAccountKeyCredentials(keyFile, "you@gmail.com");
var client = new GmailClient(credentials, "MyApp");
await client.Send(message);
```

That's all!

__!!This feature has not been fully tested yet. Any feedback is appreciated!!__

## What do you think?
Let me know what you think. Please report any issues [here](https://github.com/appie2go/Yccnl.Gmailer/issues) or create a pull request.

Cheers!
