namespace API.Configurations
{
    public class ZoomConfigurations
    {
        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
        public string AccountId { get; set; } = ""; 
        public string ApiBaseUrl { get; set; } = "https://api.zoom.us/v2";
        public string TokenEndpoint { get; set; } = "https://zoom.us/oauth/token";
    }
}
