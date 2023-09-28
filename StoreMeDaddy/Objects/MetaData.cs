namespace StoreMeDaddy.Objects;
public record MetaDataRecord(string FileName,  string Path, long Size, string Type, bool IsPublic, bool IsDeleted, string? Version, DateTime CreatedAt, DateTime ModifiedAt, DateTime AccessedAt, string UserId, string Hash, byte[] Salt, byte[] IV)
{
    public MetaDataRecord(string fileName, string path, long size, string type, bool isPublic, bool isDeleted, string? version, string userId, string hash, byte[] salt, byte[] iv)
        : this(fileName, path, size, type, isPublic, isDeleted, version, DateTime.Now, DateTime.Now, DateTime.Now, userId, hash, salt, iv)
    {
        if (string.IsNullOrEmpty(hash))
        {
            throw new ArgumentNullException(nameof(hash), "Invalid hash");
        }
        if (size <= 0)
        {
            throw new ArgumentException("Invalid file size", nameof(size));
        }
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }
        if (string.IsNullOrEmpty(type))
        {
            throw new ArgumentNullException(nameof(type));
        }
        if (hash.Length != 64)
        {
            throw new ArgumentException("Invalid hash", nameof(hash));
        }
    }
}