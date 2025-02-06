using Microsoft.Extensions.Logging;

namespace KevinApp.Models
{
    public class GoogleCalendarModel
    {
        public string? Summary { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public EventDateTime? Start { get; set; }
        public EventDateTime? End { get; set; }
    }

    public class EventDateTime
    {
        public string? DateTime { get; set; }
        public string? TimeZone { get; set; } = "Asia/Jerusalem";
    }
}