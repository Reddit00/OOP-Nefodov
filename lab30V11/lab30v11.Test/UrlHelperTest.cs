using Xunit;
using lab30v11;

namespace lab30v11.Test;

public class UrlHelperTests
{
    private readonly UrlHelper _urlHelper = new UrlHelper();

    // Тести для IsValidUrl 
    [Theory]
    [InlineData("https://google.com")]
    [InlineData("http://microsoft.com/path")]
    [InlineData("https://sub.domain.edu.ua")]
    [InlineData("https://chat.openai.com?q=test")]
    [InlineData("http://127.0.0.1")]
    public void IsValidUrl_ValidInputs_ReturnsTrue(string url)
    {
        Assert.True(_urlHelper.IsValidUrl(url));
    }

    // Тести для IsValidUrl 
    [Theory]
    [InlineData("google.com")]      // без протоколу
    [InlineData("ftp://files.com")] // непідтримуваний протокол
    [InlineData("")]               // порожній рядок
    public void IsValidUrl_InvalidInputs_ReturnsFalse(string url)
    {
        Assert.False(_urlHelper.IsValidUrl(url));
    }

    // Тести для GetDomain
    [Fact]
    public void GetDomain_StandardUrl_ReturnsHost()
    {
        var result = _urlHelper.GetDomain("https://github.com/explore");
        Assert.Equal("github.com", result);
    }

    [Fact]
    public void GetDomain_WithWww_StripsWww()
    {
        var result = _urlHelper.GetDomain("https://www.apple.com");
        Assert.Equal("apple.com", result);
    }

    [Fact]
    public void GetDomain_InvalidUrl_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _urlHelper.GetDomain("not-a-valid-url"));
    }
}