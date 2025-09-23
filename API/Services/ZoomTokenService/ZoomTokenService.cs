using API.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace API.Services.ZoomTokenService
{
    public class ZoomTokenService : IZoomTokenService
    {
        private readonly IHttpClientFactory _http;
        private readonly IMemoryCache _cache;
        private readonly ZoomConfigurations _opts;
        private readonly ILogger<ZoomTokenService> _logger;
        private const string CacheKey = "zoom_access_token";

        public ZoomTokenService(IHttpClientFactory http, IMemoryCache cache, IOptions<ZoomConfigurations> opts, ILogger<ZoomTokenService> logger)
        {
            _http = http;
            _cache = cache;
            _opts = opts.Value;
            _logger = logger;
        }

        public async Task<string> GetAccessTokenAsync(CancellationToken ct = default)
        {
            if (_cache.TryGetValue(CacheKey, out string? token) && !string.IsNullOrEmpty(token))
                return token;

            var client = _http.CreateClient();
            var basic = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{_opts.ClientId}:{_opts.ClientSecret}"));
            var req = new HttpRequestMessage(HttpMethod.Post, _opts.TokenEndpoint);

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "account_credentials",
                ["account_id"] = _opts.AccountId
            };

            _logger.LogInformation("Requesting Zoom access token with grant_type: {GrantType}, account_id: {AccountId}",
                form["grant_type"],
                string.IsNullOrEmpty(_opts.AccountId) ? "EMPTY" : "SET");

            req.Content = new FormUrlEncodedContent(form);
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", basic);
            req.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var resp = await client.SendAsync(req, ct);

            if (!resp.IsSuccessStatusCode)
            {
                var errorContent = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogError("Zoom token request failed. Status: {StatusCode}, Response: {ErrorResponse}",
                    resp.StatusCode, errorContent);

                throw new HttpRequestException($"Zoom token request failed with status {resp.StatusCode}: {errorContent}");
            }

            var payload = JsonSerializer.Deserialize<JsonElement>(await resp.Content.ReadAsStringAsync(ct));
            var accessToken = payload.GetProperty("access_token").GetString();
            var expiresIn = payload.GetProperty("expires_in").GetInt32(); // seconds

            if (string.IsNullOrEmpty(accessToken))
                throw new InvalidOperationException("Access token is null or empty");

            _cache.Set(CacheKey, accessToken, TimeSpan.FromSeconds(Math.Max(60, expiresIn - 60)));

            _logger.LogInformation("Successfully obtained Zoom access token, expires in {ExpiresIn} seconds", expiresIn);
            return accessToken;
        }

    public  string GetZoomUserId(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(accessToken);
        return jwt.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
    }

}
}