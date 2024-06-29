using Send2Print.Models.Entities;

namespace Send2Print.Models.Services.EmailService.Models;

public class MailEntity
{
    public string Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;

    public string[] To { get; set; } = [];
    public string[] Cc { get; set; } = [];
    public string Body { get; set; } = string.Empty;
    public string HtmlBody { get; set; } = string.Empty;
    public List<string> BodyParts { get; set; }
    public DateTime Date { get; set; }
    public List<EmailAttachment> Attachments { get; set; } = [];
}

public enum EmailLoadState
{
    // Email has not been loaded yet
    Preload,

    // Basic information like subject, content text, sender were loaded but no attachments and complex data
    Basic,

    // Email has attachments but still no complext info
    HasAttachments,
    AttachmentsDownloaded,
    FullyLoadedWithAttachments,
    FullyLoadedWithoutAttachments
}
