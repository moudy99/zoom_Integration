using API.Enums;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class MeetingSettings
    {
        public bool HostVideo { get; set; } = true;
        public bool ParticipantVideo { get; set; } = true;
        public bool JoinBeforeHost { get; set; } = false;
        public bool MuteUponEntry { get; set; } = true;
        public bool WaitingRoom { get; set; } = false;
        
        [JsonPropertyName("auto_recording")]
        public string? AutoRecording { get; set; } = null;
        
        public ApprovalType ApprovalType { get; set; } = ApprovalType.NoRegistrationRequired;
        public RegistrationType RegistrationType { get; set; } = RegistrationType.RegisterOnceAttendAny;
        public bool MeetingAuthentication { get; set; } = false;
    }

    public static class AutoRecordingOptions
    {
        public const string None = "none";
        public const string Local = "local";
        public const string Cloud = "cloud";
    }
}
