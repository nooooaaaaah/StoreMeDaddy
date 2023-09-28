namespace StoreMeDaddy.Controllers;
using Microsoft.AspNetCore.Mvc;
using StoreMeDaddy.Services;
using Microsoft.AspNetCore.Authorization;
using StoreMeDaddy.Objects;
using Microsoft.AspNetCore.Http;

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
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var result = await _fileUploadService.UploadFileAsync(file, User);
        return Ok(result);
    }
}