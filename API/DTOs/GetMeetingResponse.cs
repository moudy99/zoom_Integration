namespace API.DTOs
{
    public class GetMeetingResponse
    {
        public long Id { get; set; }
        public string AssistantId { get; set; } = "";
        public string HostEmail { get; set; } = "";
        public string RegistrationUrl { get; set; } = "";
        public string Agenda { get; set; } = "";
        public DateTimeOffset CreatedAt { get; set; }
        public int Duration { get; set; }
        public string EncryptedPassword { get; set; } = "";
        public string PstnPassword { get; set; } = "";
        public string H323Password { get; set; } = "";
        public string JoinUrl { get; set; } = "";
        public string ChatJoinUrl { get; set; } = "";
        public string Password { get; set; } = "";
        public string Pmi { get; set; } = "";
        public bool PreSchedule { get; set; }
        public MeetingSettings Settings { get; set; } = new();
        public DateTimeOffset StartTime { get; set; }
        public string StartUrl { get; set; } = "";
        public string Timezone { get; set; } = "";
        public string Topic { get; set; } = "";
        public int Type { get; set; }
        public string DynamicHostKey { get; set; } = "";
        public string CreationSource { get; set; } = "";
        public string Uuid { get; set; } = "";
        public string Status { get; set; } = "";
        public List<MeetingOccurrence> Occurrences { get; set; } = new();
        public MeetingRecurrence? Recurrence { get; set; }
        public List<MeetingTrackingField> TrackingFields { get; set; } = new();
    }
}