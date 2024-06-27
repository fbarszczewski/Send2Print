namespace Send2Print.Models.Services.EmailService;

public class Email
{
    public string MessageId { get; set; }
    public string From { get; set; }
    public List<string> To { get; set; } = new List<string>();
    public string Subject { get; set; }
    public string TextBody { get; set; }
    public string HtmlBody { get; set; }
    public List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
    public DateTime DateSent { get; set; }
    public DateTime DateReceived { get; set; }
    public List<string> Cc { get; set; } = new List<string>();
    public List<string> Bcc { get; set; } = new List<string>();
    public bool IsRead { get; set; }
    public bool IsImportant { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public string ReplyTo { get; set; }
    public string Folder { get; set; }
    public long Size { get; set; }
    public string Category { get; set; }
    public string ThreadId { get; set; }
    public string UID { get; set; }
}