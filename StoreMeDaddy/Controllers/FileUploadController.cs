namespace StoreMeDaddy.Controllers;
using Microsoft.AspNetCore.Mvc;
using StoreMeDaddy.Services;
using Microsoft.AspNetCore.Authorization;
using StoreMeDaddy.Classes;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FileUploadController : ControllerBase
{
    private readonly IFileUploadService _fileUploadService;

    public FileUploadController(IFileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(FileUploadModel fileUploadModel)
    {
        var result = await _fileUploadService.UploadFileAsync(fileUploadModel, User);
        return Ok(result);
    }
}