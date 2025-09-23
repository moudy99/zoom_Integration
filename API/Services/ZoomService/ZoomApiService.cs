using API.Configurations;
using API.DTOs;
using API.Services.ZoomTokenService;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace API.Services.ZoomService
{
    public class ZoomApiService : IZoomApiService
    {
        private readonly IHttpClientFactory _http;
        private readonly IMemoryCache _cache;
        private readonly IZoomTokenService zoomTokenService;
        private readonly ZoomConfigurations _opts;
        private const string CacheKey = "zoom_access_token";
        public ZoomApiService(IHttpClientFactory http, IMemoryCache cache, IOptions<ZoomConfigurations> opts , IZoomTokenService zoomTokenService)
        {
            _http = http;
            _cache = cache;
            this.zoomTokenService = zoomTokenService;
            _opts = opts.Value;
        }

      
        public async Task<CreateMeetingResponse> CreateMeetingAsync( CreateMeetingRequest request, CancellationToken ct = default)
        {
            string accessToken = await zoomTokenService.GetAccessTokenAsync(ct);
            string userId = zoomTokenService.GetZoomUserId(accessToken);
            var client = _http.CreateClient();

            var requestUrl = $"{_opts.ApiBaseUrl}/users/{userId}/meetings";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);

            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            httpRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var requestBody = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                WriteIndented = false
            });
            httpRequest.Content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(httpRequest, ct);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync(ct);
            var meetingResponse = JsonSerializer.Deserialize<CreateMeetingResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                PropertyNameCaseInsensitive = true
            });

            return meetingResponse!;
        }
    }
}