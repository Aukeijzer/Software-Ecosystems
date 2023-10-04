namespace spider.Models;

public class RateLimit
{
    public int? remaining { get; set; }
    public int? cost { get; set; }
    public int? limit { get; set; }
    public int? used { get; set; }
    public DateTime? resetAt { get; set; }
}