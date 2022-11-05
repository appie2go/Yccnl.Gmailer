# Yccnl.Gmailer

Since may 2022, Gmail no longer supports sending e-mails programatically using the SMTP protocol.

## Alternative to sending mails with SMTP

Instead you can send e-mails using the Google API. But i.m.o. this API is a pain. So, this repository is a wrapper around it. You can either grab code from it and customize it to your needs, or use this code directly by installing the [NuGet Package](https://www.nuget.org/packages/Yccnl.Gmail) that comes with this repo:


```ps
dotnet add package Yccnl.Gmail --version 0.0.2
```

## How to use it in a console or a desktop app

### Configure your gmail properly:

* Go to [https://console.cloud.google.com/apis](https://console.cloud.google.com/apis)
* Click `Credentials`, and click `Create credentials`
* Create an `OAuth Client Id` for a `Desktop App` and copy the client_id and the client_secret

### Use it without google workspaces in a desktop-app or a console app:
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

Not that using a client-id and a client-secret, when you start up your app for the first time, Google will pop up a browser window asking you to authenticate. This method is unfit for web apps. What is convenient though, is that this method does not require you to create a Google Workspace.

## Use it with server-to-server use cases

As mentioned earlier, the method above is unfit for server-to-server web apps. In that case, go to [https://console.cloud.google.com/apis](https://console.cloud.google.com/apis) and create a service account. Create a key, and download the corresponding p12 certificate or a json key. Be sure to enable domain wide delegation. How to do this exactly has been described in this article:

[https://developers.google.com/identity/protocols/oauth2/service-account#delegatingauthority](https://developers.google.com/identity/protocols/oauth2/service-account#delegatingauthority)

Sending e-mails from the server side with this library is done as follows:

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
Let me know what you think. Please report issues in the 'issues' section!
