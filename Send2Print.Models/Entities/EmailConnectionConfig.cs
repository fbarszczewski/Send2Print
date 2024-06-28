namespace Send2Print.Models.Entities;

public class EmailConnectionConfig
{
    public required string EmailAddress { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    public required string IncomingMailServer { get; set; } = string.Empty;
    public int IncomingMailPort { get; set; }
    public bool UseSsl { get; set; } = true;
    public string Protocol { get; set; } = "IMAP";
}