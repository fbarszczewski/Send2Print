namespace Send2Print.Models.Services.EmailService.Models;

public class EmailConfig
{
    public string ImapServer { get; set; }
    public int ImapPort { get; set; }
    public bool UseSsl { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public required string Username { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
}