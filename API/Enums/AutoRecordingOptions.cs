using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace API.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AutoRecordingOptions
    {
        [EnumMember(Value = "none")]
        None,

        [EnumMember(Value = "local")]
        Local,

        [EnumMember(Value = "cloud")]
        Cloud
    }
}
