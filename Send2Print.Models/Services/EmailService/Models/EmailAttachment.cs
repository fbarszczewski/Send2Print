namespace Send2Print.Models.Entities;

public class EmailAttachment
{
    public required string FileName { get; init; } = string.Empty;
    public required string FilePath { get; init; } = string.Empty;
    public long Size { get; set; }
    public string FileType => _fileType.Value;
    private readonly Lazy<string> _fileType;

    public EmailAttachment()
    {
        _fileType = new Lazy<string>(() => Path.GetExtension(FilePath).TrimStart('.').ToLower());
    }

    private static string DetermineFileType(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)) return "";

        var extension = Path.GetExtension(filePath).TrimStart('.').ToLower();

        return string.IsNullOrEmpty(extension) ? "" : extension;
    }
}