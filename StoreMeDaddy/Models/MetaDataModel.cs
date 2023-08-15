namespace StoreMeDaddy.Models;
public class MetaDataModel
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string Description { get; set; }
    public string Path { get; set; }
    public long Size { get; set; }
    public string Type { get; set; }
    public bool IsPublic { get; set; }
    public bool IsDeleted { get; set; }
    public string Version { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime AccessedAt { get; set; }
    public string CreatedByUser { get; set; }
    public string Hash { get; set; }

    public MetaDataModel(string FileName, string description, string path, long size, string type, bool isPublic, bool isDeleted, string? version, string createdByUser, string hash)
    {

        if (string.IsNullOrEmpty(hash))
        {
            throw new ArgumentNullException(nameof(hash), "Invalid hash");
        }
        this.FileName = FileName ?? throw new ArgumentNullException(nameof(FileName));
        Description = description ?? "";
        Path = path ?? throw new ArgumentNullException(nameof(path));
        Size = size > 0 ? size : throw new ArgumentException("Invalid file size", nameof(size));
        Type = type ?? throw new ArgumentNullException(nameof(type));
        IsPublic = isPublic;
        IsDeleted = isDeleted;
        Version = version ?? "1";
        CreatedAt = DateTime.Now;
        ModifiedAt = CreatedAt;
        AccessedAt = CreatedAt;
        CreatedByUser = createdByUser ?? throw new ArgumentNullException(nameof(createdByUser));
        Hash = hash.Length != 64 ? throw new ArgumentException("Invalid hash", nameof(hash)) : hash;
    }
    public override string ToString()
    {
        return $"{FileName},{Description},{Path},{Size},{Type},{IsPublic},{IsDeleted},{Version},{CreatedAt},{ModifiedAt},{AccessedAt},{CreatedByUser},{Hash}";
    }
}




