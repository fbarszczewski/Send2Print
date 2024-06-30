using System.Net.Mail;
using Send2Print.Models.Entities;
using Send2Print.Models.Services.EmailService.Models;

namespace Send2Print.Models.Services.EmailService.Interfaces;

public interface IImapService
{
    /// <summary>
    /// Connects to the IMAP server.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    Task ConnectAsync();

    /// <summary>
    /// Disconnects from the IMAP server.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    Task DisconnectAsync();

    /// <summary>
    /// Fetches emails from the IMAP server.
    /// </summary>
    /// <param name="since">The minimum date for emails to fetch.</param>
    /// <returns>Asynchronous task returning a list of emails.</returns>
    Task<List<EmailEntity>> FetchEmailsAsync(DateTime since);

    /// <summary>
    /// Fetches attachments for a given email message.
    /// </summary>
    /// <param name="messageId">The ID of the email message.</param>
    /// <returns>Asynchronous task returning a list of attachments.</returns>
    Task<List<EmailAttachment>> FetchAttachmentsAsync(string messageId);

    /// <summary>
    /// Marks an email message as read.
    /// </summary>
    /// <param name="messageId">The ID of the email message to mark as read.</param>
    /// <returns>Asynchronous task.</returns>
    Task MarkAsReadAsync(string messageId);

    /// <summary>
    /// Marks an email message as unread.
    /// </summary>
    /// <param name="messageId">The ID of the email message to mark as unread.</param>
    /// <returns>Asynchronous task.</returns>
    Task MarkAsUnreadAsync(string messageId);
}