using FileStorage.BO;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileStorage.Controllers;

[ApiController]
[Route("[controller]")]
public class FileStorageController : ControllerBase
{
    
    private readonly IFileUploadAdapter _fileUploadAdapter;

    public FileStorageController(IFileUploadAdapter fileUploadAdapter)
    {
        _fileUploadAdapter = fileUploadAdapter;
    }
    
    /// <summary> Добавление файла. На вход принимает объект UserFile. Возвращает Id </summary>
    [HttpPost(Name = "UploadImage")]
    public async Task<string> UploadImageAsync([FromForm] string userId, IFormFile file)
    {
        file = HttpContext.Request.Form.Files[0];
        var userFile = new UserFile()
        {
            Name = file.FileName,
            UserId = userId
        };
        using (var ms = new MemoryStream())
        {
            file.CopyTo(ms);
            var fileBytes = ms.ToArray();
            userFile.File = fileBytes;
        }

        var result = await _fileUploadAdapter.SaveFileAsync(userFile);
        
        return result;
    }
    
    /// <summary> Возвращает файл по его Id </summary>
    [HttpGet(Name = "GetById")]
    public  async Task<IActionResult>  GetById(string id){

        var file =  await _fileUploadAdapter.GetFileByIdAsync(id);
        
        return File(file.File, "application/octet-stream", fileDownloadName: file.Name);
    }
}