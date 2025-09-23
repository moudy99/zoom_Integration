using API.DTOs;

namespace API.Services.ZoomService
{
    public interface IZoomApiService
    {
        Task<CreateMeetingResponse> CreateMeetingAsync(CreateMeetingRequest request, CancellationToken ct = default);
    }
}
