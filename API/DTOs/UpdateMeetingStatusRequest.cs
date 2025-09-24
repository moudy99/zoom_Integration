namespace API.DTOs
{
    public class UpdateMeetingStatusRequest
    {
        public string Action { get; set; } = "";
    }

    public static class MeetingStatusActions
    {
        public const string End = "end";
        public const string Recover = "recover";
    }
}