using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using Send2Print.Models.Services.EmailService.Interfaces;
using Send2Print.Models.Services.EmailService.Models;

namespace Send2Print.Models.Services.EmailService;

public class EmailService 
{
    private List<ImapClient> _imapClientList;
    private List<SmtpClient> _smtpClientList;
    private bool _isConnected = false;

    public void Connect(EmailServerConfigs configs)
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

    public async IAsyncEnumerable<EmailMessageInfo> GetMessagesAsync(string folderName, int startIndex, int count)
    {
        var folder = await _client.GetFolderAsync(folderName);
        await folder.OpenAsync(FolderAccess.ReadOnly);

        for (int i = startIndex; i < startIndex + count; i++)
        {
            var message = await folder.GetMessageAsync(i);
            var basicInfo = new EmailMessageInfo
            {
                Subject = message.Subject,
                From = message.From.ToString(),
                TextBody = message.TextBody
            };

            yield return basicInfo;

            var attachmentPaths = new List<string>();
            if (message.Attachments != null)
            {
                foreach (var attachment in message.Attachments)
                {
                    if (attachment is MimePart mimePart)
                    {
                        var filePath = Path.Combine("path/to/attachments", mimePart.FileName);
                        using (var stream = File.Create(filePath))
                        {
                            await mimePart.Content.DecodeToAsync(stream);
                        }
                        attachmentPaths.Add(filePath);
                    }
                }
            }

            basicInfo.AttachmentPaths = attachmentPaths;
            yield return basicInfo;

            basicInfo.FullMessage = message;
            yield return basicInfo;
        }
    }

    public async Task<EmailEntity> GetEmailAsync(string id)
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
                return new EmailEntity
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