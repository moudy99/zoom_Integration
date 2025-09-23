namespace API.Services.ZoomTokenService
{
    public interface IZoomTokenService
    {
        Task<string> GetAccessTokenAsync(CancellationToken ct = default);
        string GetZoomUserId(string accessToken);
    }
}
