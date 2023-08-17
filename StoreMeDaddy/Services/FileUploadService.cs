namespace StoreMeDaddy.Services;

using StoreMeDaddy.Classes;
using StoreMeDaddy.Models;
using HashSlingingSlasher;
using System.Security.Claims;

public interface IFileUploadService
{
    Task<MetaDataModel> UploadFileAsync(FileUploadModel fileUploadModel, ClaimsPrincipal principal);
}

public class FileUploadService : IFileUploadService
{
    private readonly StoreMeDaddyContext _context;
    private readonly string _uploadPath; 

    public FileUploadService(StoreMeDaddyContext context, string uploadPath)
    {
        _context = context;
        _uploadPath = uploadPath;
    }

    public async Task<MetaDataModel> UploadFileAsync(FileUploadModel fileUploadModel, ClaimsPrincipal principal)
    {
        string userIdClaim = (principal.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new Exception("User ID claim not found in principal.");
        string hash = Hasher.HashFile(fileUploadModel.File);
        MetaDataModel metadata = new(fileUploadModel.File.FileName, fileUploadModel.Description, _uploadPath, fileUploadModel.Size, fileUploadModel.Type, fileUploadModel.IsPublic, false,  fileUploadModel.Version, userIdClaim, hash);

        // Save the file to the disk
        string filePath = Path.Combine(_uploadPath, metadata.FileName);
        using (FileStream fileStream = new(filePath, FileMode.Create))
        {
            await fileUploadModel.File.CopyToAsync(fileStream);
        }

        // Save the metadata to the database
        _context.MetaData.Add(metadata);
        await _context.SaveChangesAsync();

        return metadata;
    }
}