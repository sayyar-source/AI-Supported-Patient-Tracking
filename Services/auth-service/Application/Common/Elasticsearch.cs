namespace Auth.Application.Common;
public class AuthLog
{
    public string Email { get; set; }
    public string Action { get; set; } // "Login", "Register"
    public bool Success { get; set; }
    public string IpAddress { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
