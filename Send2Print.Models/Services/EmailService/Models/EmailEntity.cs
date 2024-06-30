using Send2Print.Models.Entities;

namespace Send2Print.Models.Services.EmailService.Models;

public class EmailEntity
{
    public string Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string[] To { get; set; } = [];
    public string Author { get; set; } = string.Empty;
    public string[] Cc { get; set; } = [];
    public string TextBody { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Folder { get; set; }
}
