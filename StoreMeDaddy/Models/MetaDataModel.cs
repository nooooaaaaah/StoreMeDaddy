namespace StoreMeDaddy.Models;
public class MetaDataModel
{
    public string Name { get; set; }
    public string Path { get; set; }
    public long Size { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime AccessedAt { get; set; }
    public string CreatedByUser { get; set; }
    public string Hash { get; set; }

    public MetaDataModel(string name, string path, long size, string type, DateTime createdAt, DateTime modifiedAt, DateTime accessedAt, string createdByUser, string hash)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(path))
            throw new ArgumentNullException(nameof(path));
        if (string.IsNullOrEmpty(type))
            throw new ArgumentNullException(nameof(type));
        if (string.IsNullOrEmpty(createdByUser))
            throw new ArgumentNullException(nameof(createdByUser));
        if (createdAt > DateTime.Now || createdAt > modifiedAt || createdAt > accessedAt)
            throw new ArgumentException("Invalid date", nameof(createdAt));
        if (modifiedAt > DateTime.Now || modifiedAt > accessedAt)
            throw new ArgumentException("Invalid date", nameof(modifiedAt));
        if (accessedAt > DateTime.Now)
            throw new ArgumentException("Invalid date", nameof(accessedAt));
        if (size < 0)
            throw new ArgumentException("Invalid size", nameof(size));
        if (string.IsNullOrEmpty(hash))
            throw new ArgumentNullException(nameof(hash));
        if (hash.Length != 64)
            throw new ArgumentException("Invalid hash", nameof(hash));
        Name = name;
        Path = path;
        Size = size;
        Type = type;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        AccessedAt = accessedAt;
        CreatedByUser = createdByUser;
        Hash = hash;
    }
    public override string ToString()
    {
        return $"{Name},{Path},{Size},{Type},{CreatedAt},{ModifiedAt},{AccessedAt},{CreatedByUser},{Hash}";
    }
}




