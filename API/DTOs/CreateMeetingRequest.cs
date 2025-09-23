using API.Enums;

namespace API.DTOs
{
    public class CreateMeetingRequest
    {
        public string Topic { get; set; } = "New Meeting";
        public MeetingType Type { get; set; } = MeetingType.Scheduled;
        public string StartTime { get; set; } = DateTimeOffset.UtcNow.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ssZ");
        public int Duration { get; set; }
        public string Timezone { get; set; } = "UTC";
        public string Agenda { get; set; } = "";
        public string Password { get; set; } = "";
        public string AlternativeHosts { get; set; } = "";
        public bool PreSchedule { get; set; } = false;
        public MeetingSettings Settings { get; set; } = new MeetingSettings();
        public MeetingRecurrence? Recurrence { get; set; }
        public List<MeetingTrackingField> TrackingFields { get; set; } = new();
    }
}
