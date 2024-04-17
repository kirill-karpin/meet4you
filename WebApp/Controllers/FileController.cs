using Microsoft.AspNetCore.Mvc;
using WebApp.Service;


namespace WebApp.Controllers;

[ApiController]
[Route("/api/files")]
public class FileController : Controller
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<string> SaveFile(Stream stream)
    {
        return await _fileService.SaveFile(stream);
    }

    public async Task<byte> GetFileById(string id)
    {
        return await _fileService.GetFileById(id);
    }

    public async Task<byte> RemoveFile(string id)
    {
        return await  _fileService.RemoveFile(id);
    }
}