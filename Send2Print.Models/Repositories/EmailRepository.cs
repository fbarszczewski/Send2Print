using Send2Print.Models.Services.EmailService.Models;

namespace Send2Print.Models.Repositories;

public class EmailRepository
{
    private readonly Dictionary<string, List<EmailEntity>> _emailAccounts;
}