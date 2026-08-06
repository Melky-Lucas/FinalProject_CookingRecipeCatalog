namespace WebAPI.Models
{
    public class CustomProblemDetails
    {
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int Status { get; set; }
        public string Detail { get; set; } = null!;
        public string Instance { get; set; } = null!;
        public string TraceId { get; set; } = null!;
        public DateTime Timestamp { get; set; }

        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
