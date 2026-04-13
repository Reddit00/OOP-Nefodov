namespace lab30v11;

public class UrlHelper
{
    public bool IsValidUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

   public string GetDomain(string url)
    {
        if (!IsValidUrl(url))
            throw new ArgumentException("Invalid URL format");

        var uri = new Uri(url);
        var host = uri.Host;

        return host.StartsWith("www.", StringComparison.OrdinalIgnoreCase) 
               ? host[4..] 
               : host;
    }
}