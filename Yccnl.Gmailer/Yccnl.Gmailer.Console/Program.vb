Imports System.Net.Mail

Module Program
    Sub Main(args As String())
        System.Console.WriteLine("---------------------------------")
        System.Console.WriteLine("     Welcome to the gMailer!     ")
        System.Console.WriteLine("---------------------------------")
        
        System.Console.WriteLine("Please enter the client_id and hit enter:")
        Dim clientId As String = System.Console.ReadLine()
        
        System.Console.WriteLine("Please enter the client_secret and hit enter:")
        Dim clientSecret As String = System.Console.ReadLine()
        
        System.Console.WriteLine("Please enter your gmail address and hit enter:")
        Dim gmailAddress As String = System.Console.ReadLine()
        
        System.Console.WriteLine("Please enter the e-mail recipient and hit enter:")
        Dim recipient As String = System.Console.ReadLine()
        
        System.Console.WriteLine("Please enter the e-mail subject and hit enter:")
        Dim subject As String = System.Console.ReadLine()
        
        System.Console.WriteLine("Please enter the e-mail body and hit enter:")
        Dim body As String = System.Console.ReadLine()

        Dim message = new MailMessage()
        message.To.Add(recipient)
        message.Subject = subject
        message.Body = body
        
        Dim credentials = new ClientIdAndClientSecret(clientId, clientSecret, gmailAddress)
        Dim gMailer = new GmailClient(credentials, "testClient")
        
        System.Console.WriteLine("Sending e-mail")
        gMailer.Send(message).Wait()
        
        System.Console.WriteLine("Done..")
    End Sub
End Module
