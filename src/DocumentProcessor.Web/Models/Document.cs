namespace DocumentProcessor.Web.Models;

public enum DocumentStatus { Pending, Processing, Processed, Failed }

public class Document
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string StoragePath { get; set; } = string.Empty;
    public DocumentStatus Status { get; set; }
    public string? Summary { get; set; }
    public string UploadedBy { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
