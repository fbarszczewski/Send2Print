namespace Send2Print.Models.Entities;

public class MailEntity
{
    public uint Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public List<string> To { get; set; } = [];
    public List<string> Cc { get; set; } = [];
    public string Body { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public List<EmailAttachment> Attachments { get; set; } = [];
}