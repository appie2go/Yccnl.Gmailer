using System;
using FluentAssertions;
using Xunit;

namespace Yccnl.Gmailer.Tests;

public class ClientIdAndClientSecretTests
{
    private const string ClientId = "x";
    private const string ClientSecret = "x";
    private const string FromAddress = "test@domain.com";
    
    [Fact]
    public void WhenNoClientId_ShouldThrowArgumentException()
    {
        var actual = () => new ClientIdAndClientSecret(null, ClientSecret, FromAddress);

        actual.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void WhenEmptyClientId_ShouldThrowArgumentException()
    {
        var actual = () => new ClientIdAndClientSecret(string.Empty, ClientSecret, FromAddress);

        actual.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void WhenNoClientSecret_ShouldThrowArgumentException()
    {
        var actual = () => new ClientIdAndClientSecret(ClientId, null, FromAddress);

        actual.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void WhenEmptyClientSecret_ShouldThrowArgumentException()
    {
        var actual = () => new ClientIdAndClientSecret(ClientId, string.Empty, FromAddress);

        actual.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void WhenNoFromAddress_ShouldThrowArgumentException()
    {
        var actual = () => new ClientIdAndClientSecret(ClientId, ClientSecret, null);

        actual.Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("foo")]
    [InlineData("foo@")]
    public void WhenInvalidFromAddress_ShouldThrowArgumentException(string invalidFromAddress)
    {
        var actual = () => new ClientIdAndClientSecret(ClientId, ClientSecret, invalidFromAddress);

        actual.Should().Throw<ArgumentException>();
    }
}