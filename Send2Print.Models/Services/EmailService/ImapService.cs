using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using Send2Print.Models.Entities;
using Send2Print.Models.Services.EmailService.Models;

namespace Send2Print.Models.Services.EmailService.Interfaces;

public class ImapService(EmailServerConfig emailConfig) 
{
    private readonly EmailServerConfig _emailConfig = emailConfig;
    private ImapClient _imapClient = null!;
    public async Task ConnectAsync()
    {
        _imapClient = new ImapClient();
        _imapClient = new ImapClient();
        await _imapClient.ConnectAsync(_emailConfig.ImapServer, _emailConfig.ImapPort, _emailConfig.UseSsl);
        await _imapClient.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);
    }
    
    public async Task DisconnectAsync()
    {
        await _imapClient.DisconnectAsync(true);
        _imapClient.Dispose();
    }
    
    public async Task<IEnumerable<string>> GetFoldersAsync()
    {
        var personalNamespace = _imapClient.PersonalNamespaces[0];
        var folders = await _imapClient.GetFoldersAsync(personalNamespace);

        return folders.Select(f => f.Name);
    }


    public async IAsyncEnumerable<MimeMessage> GetMessagesAsync(string folderName, int startIndex, int count)
    {
        var folder = await _imapClient.GetFolderAsync(folderName);
        await folder.OpenAsync(FolderAccess.ReadOnly);

        for (int i = startIndex; i < startIndex + count; i++)
        {
            var message = await folder.GetMessageAsync(i);
            yield return message;
        }
    }

    public async Task MarkAsReadAsync(string messageId, string folderName)
    {

    }

    public async Task MarkAsUnreadAsync(string messageId, string folderName)
    {

    }
}