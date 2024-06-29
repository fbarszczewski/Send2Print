using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using Send2Print.Models.Services.EmailService.Interfaces;
using Send2Print.Models.Services.EmailService.Models;

namespace Send2Print.Models.Services.EmailService;

public class EmailService : IEmailService
{
    private List<ImapClient> _imapClientList;
    private List<SmtpClient> _smtpClientList;
    private bool _isConnected = false;

    public void Connect(EmailMultiConfig configs)
    {
        _imapClientList = new List<ImapClient>();
        _smtpClientList = new List<SmtpClient>();

        foreach (var config in configs.EmailAccounts)
        {
            var imapClient = new ImapClient();
            var smtpClient = new SmtpClient();

            imapClient.Connect(config.ImapServer, config.ImapPort, config.UseSsl);
            imapClient.Authenticate(config.Username, config.Password);

            smtpClient.Connect(config.SmtpServer, config.SmtpPort, config.UseSsl);
            smtpClient.Authenticate(config.Username, config.Password);

            _imapClientList.Add(imapClient);
            _smtpClientList.Add(smtpClient);
        }

        _isConnected = true;
    }

    public async Task<IEnumerable<MailEntity>> GetEmailsAsync(DateTime startDate, DateTime endDate)
    {
        if (!_isConnected)
            throw new InvalidOperationException("EmailService is not connected.");

        var emails = new List<MailEntity>();

        foreach (var imapClient in _imapClientList)
        {
            var inbox = imapClient.Inbox;
            await inbox.OpenAsync(FolderAccess.ReadOnly);
            var query = SearchQuery.DeliveredAfter(startDate).And(SearchQuery.DeliveredBefore(endDate));
            var uids = await inbox.SearchAsync(query);

            foreach (var uid in uids)
            {
                var message = await inbox.GetMessageAsync(uid);
                emails.Add(new MailEntity
                {

                    Id = message.MessageId,
                    Sender = message.From.ToString(),
                    To = message.To.Select(to => to.ToString()).ToArray(),
                    Cc = message.Cc.Select(cc => cc.ToString()).ToArray(),
                    Subject = message.Subject,
                    Body = message.TextBody,
                    HtmlBody = message.HtmlBody,
                    HtmlBody = message.,
                    Date = message.Date.DateTime
                    //,Attachments = GetAttachments(message) //TODO: Implement GetAttachments
                });
            }
        }

        return emails;
    }

    public async Task<MailEntity> GetEmailAsync(string id)
    {
        if (!_isConnected)
            throw new InvalidOperationException("EmailService is not connected.");

        foreach (var imapClient in _imapClients)
        {
            var inbox = imapClient.Inbox;
            await inbox.OpenAsync(FolderAccess.ReadOnly);

            var query = SearchQuery.HeaderContains("Message-Id", id);
            var uids = await inbox.SearchAsync(query);

            foreach (var uid in uids)
            {
                var message = await inbox.GetMessageAsync(uid);
                return new MailEntity
                {
                    Id = message.MessageId,
                    Sender = message.From.ToString(),
                    Recipient = message.To.ToString(),
                    Subject = message.Subject,
                    Body = message.TextBody,
                    Attachments = GetAttachments(message)
                };
            }
        }

        return null;
    }

}