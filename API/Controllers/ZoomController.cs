using API.DTOs;
using API.Services.ZoomService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoomController : ControllerBase
    {
        private readonly IZoomApiService _zoom;

        public ZoomController(IZoomApiService zoom) => _zoom = zoom;

        [HttpPost("create-meeting")]
        public async Task<IActionResult> CreateMeeting([FromBody] CreateMeetingRequest req, CancellationToken ct)
        {
            var result = await _zoom.CreateMeetingAsync( req, ct);
            return Ok(result);
        }

        [HttpPut("meetings/{meetingId}/status")]
        public async Task<IActionResult> UpdateMeetingStatus(long meetingId, [FromBody] UpdateMeetingStatusRequest req, CancellationToken ct)
        {
            await _zoom.UpdateMeetingStatusAsync(meetingId, req, ct);
            return NoContent();
        }
    }
}
