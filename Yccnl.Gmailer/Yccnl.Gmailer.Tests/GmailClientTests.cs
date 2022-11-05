using System;
using FluentAssertions;
using Xunit;

namespace Yccnl.Gmailer.Tests;

public partial class GmailClientTests
{
    private readonly ICredentials _credentials = new ClientIdAndClientSecret("test", "test", "test@domain.com");
    private readonly string FromAddress = "test@domain.com";
    
    [Fact]
    public void WhenNoCredentials_ShouldThrowArgumentException()
    {
        Action actual = () => new GmailClient(null, FromAddress);

        actual.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void WhenNoAppName_ShouldThrowArgumentException()
    {
        Action actual = () => new GmailClient(_credentials, null);

        actual.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void WhenEmptyAppName_ShouldThrowArgumentException()
    {
        Action actual = () => new GmailClient(_credentials, string.Empty);

        actual.Should().Throw<ArgumentException>();
    }
}