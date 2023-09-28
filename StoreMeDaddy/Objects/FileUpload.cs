namespace StoreMeDaddy.Objects;
using Microsoft.AspNetCore.Http;
// Ef Core model for file upload
public class FileUploadModel
{
    public  IFormFile File { get; set; }
    public string FileName { get; set; }
    public bool IsPublic { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? Version { get; set; }
    public long Size { get; set; }
    public string Type { get;  }
    public int OwnerId { get; set; }

    public FileUploadModel(IFormFile file, string fileName, string? description, int? folderId, bool isPublic, DateTime? expirationDate, string? version, int ownerId)
    {
        File = file ?? throw new ArgumentNullException(nameof(file));
        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        IsPublic = isPublic;
        ExpirationDate = expirationDate;
        Version = version;
        Size = file.Length;
        Type = file.ContentType;
        OwnerId = ownerId;
    }
}
