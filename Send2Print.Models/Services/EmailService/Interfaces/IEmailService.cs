using Send2Print.Models.Services.EmailService.Models;

namespace Send2Print.Models.Services.EmailService.Interfaces;

internal interface IEmailService
{
    Task ConnectAsync(EmailMultiConfig config);
    Task<IEnumerable<MailEntity>> GetEmailsAsync(DateTime startDate, DateTime endDate);
    Task<MailEntity> GetEmailAsync(string id);
    Task SendEmailAsync(MailEntity email);
    Task DisconnectAsync();
}