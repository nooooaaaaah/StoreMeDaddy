namespace StoreMeDaddy.Services;

using StoreMeDaddy.Objects;
using StoreMeDaddy.Models;
using HashSlingingSlasher;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

public interface IFileUploadService
{
    Task<MetaDataRecord> UploadFileAsync(IFormFile file, ClaimsPrincipal principal);
}

public class FileUploadService : IFileUploadService
{
    private readonly StoreMeDaddyContext _context;
    private readonly string _uploadPath;
    private readonly Aes _aes;

    public FileUploadService(StoreMeDaddyContext context, string uploadPath)
    {
        _context = context;
        _uploadPath = uploadPath;
        _aes = Aes.Create();
    }

    public async Task<MetaDataRecord> UploadFileAsync(IFormFile file, ClaimsPrincipal principal)
    {
        string userIdFromClaim = (principal.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new Exception("User ID claim not found in principal.");
        string hash = Hasher.HashFile(file);

        // Generate a new unique salt for this file
        byte[] salt = new byte[16];
        RandomNumberGenerator.Fill(salt);

        // Generate a new unique IV for this encryption operation
        _aes.GenerateIV();

        string masterKey = "YourSecureMasterKeyHere"; //todo add .env for master key
        using (var keyDerivation = new Rfc2898DeriveBytes(masterKey, salt, 100000, HashAlgorithmName.SHA256))
        {
            _aes.Key = keyDerivation.GetBytes(32); // Get a 256-bit key
        }

        string filePath = Path.Combine(_uploadPath, file.FileName);
        using (FileStream fileStream = new(filePath, FileMode.Create))
        {
            using CryptoStream cryptoStream = new(fileStream, _aes.CreateEncryptor(), CryptoStreamMode.Write);
            await file.CopyToAsync(cryptoStream);
        }

        MetaDataRecord metadataRecord = new(file.FileName, _uploadPath, file.Length, file.ContentType, false, false, "0", userIdFromClaim, hash, salt, _aes.IV);
        MetaDataModel metadataModel = new()
        {
            FileName = metadataRecord.FileName,
            Path = metadataRecord.Path,
            Size = metadataRecord.Size,
            Type = metadataRecord.Type,
            IsPublic = metadataRecord.IsPublic,
            IsDeleted = metadataRecord.IsDeleted,
            Version = metadataRecord.Version ?? "0",
            UserId = metadataRecord.UserId,
            Hash = metadataRecord.Hash,
            Salt = metadataRecord.Salt,
            IV = metadataRecord.IV
        };

        _context.MetaData.Add(metadataModel);
        await _context.SaveChangesAsync();

        return metadataRecord;
    }
}