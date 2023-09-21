namespace StoreMeDaddy.Services;

using StoreMeDaddy.Classes;
using StoreMeDaddy.Models;
using HashSlingingSlasher;
using System.Security.Claims;
using System.Security.Cryptography;

public interface IFileUploadService
{
    Task<MetaDataModel> UploadFileAsync(FileUploadModel fileUploadModel, ClaimsPrincipal principal);
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

    public async Task<MetaDataModel> UploadFileAsync(FileUploadModel fileUploadModel, ClaimsPrincipal principal)
    {
        string userIdClaim = (principal.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new Exception("User ID claim not found in principal.");
        string hash = Hasher.HashFile(fileUploadModel.File);

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
        MetaDataModel metadata = new(fileUploadModel.File.FileName, fileUploadModel.Description, _uploadPath, fileUploadModel.Size, fileUploadModel.Type, fileUploadModel.IsPublic, false, fileUploadModel.Version, userIdClaim, hash, salt, _aes.IV);
        // Save the file to the disk with encryption
        string filePath = Path.Combine(_uploadPath, metadata.FileName);
        using (FileStream fileStream = new(filePath, FileMode.Create))
        {
            using CryptoStream cryptoStream = new(fileStream, _aes.CreateEncryptor(), CryptoStreamMode.Write);
            await fileUploadModel.File.CopyToAsync(cryptoStream);
        }

        // Save salt and IV to the metadata
        metadata.Salt = salt;
        metadata.IV = _aes.IV;

        // Save the metadata to the database
        _context.MetaData.Add(metadata);
        await _context.SaveChangesAsync();

        return metadata;
    }
}