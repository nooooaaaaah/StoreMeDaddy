namespace StoreMeDaddy.Classes;
using Microsoft.AspNetCore.Http;

public class FileUploadModel
{
    public  IFormFile File { get; set; }
    public string FileName { get; set; }
    public string Description { get; set; }
    public int? FolderId { get; set; }
    public bool IsPublic { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? Version { get; set; }
    public string Hash { get; set; }
    public int OwnerId { get; set; }

    public FileUploadModel(IFormFile file, string fileName, string? description, int? folderId, bool isPublic, DateTime? expirationDate, string? version, string hash, int ownerId)
    {
        File = file ?? throw new ArgumentNullException(nameof(file));
        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        Description = description ?? "";
        FolderId = folderId;
        IsPublic = isPublic;
        ExpirationDate = expirationDate;
        Version = version;
        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
        OwnerId = ownerId;
    }
}
