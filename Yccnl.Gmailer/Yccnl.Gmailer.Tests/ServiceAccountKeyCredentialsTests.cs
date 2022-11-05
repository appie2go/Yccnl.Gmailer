using System;
using FluentAssertions;
using Xunit;

namespace Yccnl.Gmailer.Tests;

public class ServiceAccountKeyCredentialsTests
{
    private byte[] Bytes = Convert.FromBase64String(
        "ewogICJ0eXBlIjogInNlcnZpY2VfYWNjb3VudCIsCiAgInByb2plY3RfaWQiOiAieWNjbmwtMTU4ODQzNTQ2NjYzNjkiLAogICJwcml2YXRlX2tleV9pZCI6ICJkc2dhMjM0MmZhcmczMjRlcmdnc2RmZ3NkZiIsCiAgInByaXZhdGVfa2V5IjogIm51bmFqYWJlZXp3YXgiLAogICJjbGllbnRfZW1haWwiOiAiZm9vQGJhci5jb20iLAogICJjbGllbnRfaWQiOiAiMTEzNjM0MzQ2NTg1MzE2Mzg2NTEiLAogICJhdXRoX3VyaSI6ICJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20vby9vYXV0aDIvYXV0aCIsCiAgInRva2VuX3VyaSI6ICJodHRwczovL29hdXRoMi5nb29nbGVhcGlzLmNvbS90b2tlbiIsCiAgImF1dGhfcHJvdmlkZXJfeDUwOV9jZXJ0X3VybCI6ICJodHRwczovL3d3dy5nb29nbGVhcGlzLmNvbS9vYXV0aDIvdjEvY2VydHMiLAogICJjbGllbnRfeDUwOV9jZXJ0X3VybCI6ICJodHRwczovL3d3dy5nb29nbGVhcGlzLmNvbS9yb2JvdC92MS9tZXRhZGF0YS94NTA5L3ljY25sLWFsYmVydDIlNDB5Y2NubC0xNTg4NDM1NDY2NjM2OS5pYW0uZ3NlcnZpY2VhY2NvdW50LmNvbSIKfQ==");

    private const string FromAddress = "test@domain.com";
    
    [Fact]
    public void WhenNoKeyFile_ShouldThrowArgumentException()
    {
        var actual = () => new ServiceAccountKeyCredentials(null, FromAddress);

        actual.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void WhenNoEmailAddress_ShouldThrowArgumentException()
    {
        var actual = () => new ServiceAccountKeyCredentials(Bytes, null);

        actual.Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("foo")]
    [InlineData("foo@")]
    public void WhenInvalidFromAddress_ShouldThrowArgumentException(string invalidFromAddress)
    {
        var actual = () => new ServiceAccountKeyCredentials(Bytes, invalidFromAddress);

        actual.Should().Throw<ArgumentException>();
    }
}