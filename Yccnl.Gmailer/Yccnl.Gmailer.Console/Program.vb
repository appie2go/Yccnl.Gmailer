Imports System.Net.Mail

Module Program
    Sub Main(args As String())
        System.Console.WriteLine("---------------------------------")
        System.Console.WriteLine("     Welcome to the gMailer!     ")
        System.Console.WriteLine("---------------------------------")
        
        ' ======================== Compose the e-mail ==========================
        
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
        
        
        ' ============================= Authorize ==============================
        Dim credentials As ICredentials
        
        System.Console.Write("Do you want to connect to the gMail api with a client_id and a client_secret? (y/n): ")
        Dim yesNo = System.Console.ReadLine()
        
        If yesNo.Trim().Equals("y", StringComparison.InvariantCultureIgnoreCase)
            System.Console.WriteLine("You are going to connect to the gMail api with a client_id and a client_secret.")
            
            System.Console.WriteLine("Please enter the client_id and hit enter:")
            Dim clientId As String = System.Console.ReadLine()
        
            System.Console.WriteLine("Please enter the client_secret and hit enter:")
            Dim clientSecret As String = System.Console.ReadLine()
        
            System.Console.WriteLine("Please enter your gmail address and hit enter:")
            Dim gmailAddress As String = System.Console.ReadLine()
            
            credentials = new ClientIdAndClientSecret(clientId, clientSecret, gmailAddress)
        Else
            System.Console.WriteLine("You are going to connect to the gMail api with a service account key.")
            
            System.Console.WriteLine("Please enter the path to the p12 or json key-file and hit enter:")
            Dim path As String = System.Console.ReadLine()
            
            System.Console.WriteLine("Please enter your gmail address the key-file impersonates for and hit enter:")
            Dim gmailAddress As String = System.Console.ReadLine()
            
            Dim buffer = IO.File.ReadAllBytes(path)
            credentials = New ServiceAccountKeyCredentials(buffer, gmailAddress)
        End If
        
        ' ============================= Send mail ==============================
        
        Dim gMailer = new GmailClient(credentials, "testClient")
        
        System.Console.WriteLine("Sending e-mail")
        gMailer.Send(message).Wait()
        
        System.Console.WriteLine("Done..")
    End Sub
End Module
