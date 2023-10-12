namespace spider.Models;

public class RateLimit
{
    public int? Remaining { get; set; }
    public int? Cost { get; set; }
    public int? Limit { get; set; }
    public int? Used { get; set; }
    public DateTime? ResetAt { get; set; }
}