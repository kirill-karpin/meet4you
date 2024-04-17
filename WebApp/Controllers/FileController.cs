using Microsoft.AspNetCore.Mvc;
using WebApp.Service;


namespace WebApp.Controllers;

[ApiController]
[Route("files")]
public class FileController : Controller
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost]
    [Route("add-file")]
    public async Task<string> SaveFile([FromForm] string userId, IFormFile file)
    {
        file = HttpContext.Request.Form.Files[0];

        byte[] array;
        using (var ms = new MemoryStream())
        {
            file.CopyTo(ms);
            var fileBytes = ms.ToArray();
            array = fileBytes;
        }

        return await _fileService.SaveFile(array);
    }
}