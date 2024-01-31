using FileStorage.BO;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileStorage.Controllers;

[ApiController]
[Route("[controller]")]
public class FileStorageController : ControllerBase
{
    private const string mongoCnStr = "mongodb://meetforyou:meetforyou@localhost:27017/?authSource=meetforyou_files";
    private IMongoDatabase _mongoDB;
    
    private readonly ILogger<WeatherForecastController> _logger;
    public FileStorageController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        MongoClient client = new MongoClient(mongoCnStr);
        _mongoDB = client.GetDatabase("meetforyou_files");
    }

    /*
    /// <summary> Row добавление файла. На вход принимает... файл! Возвращает хэш </summary>
    [HttpPost(Name = "PostFile")]
    public string PostFile(Stream fileStream)
    {
        return "";
    }
    */
    
    /// <summary> Добавление файла. На вход принимает объект UserFile. Возвращает хэш </summary>
    [HttpPost(Name = "PostUserFile")]
    public string PostUserFile(UserFile file)
    {
        var uFiles = _mongoDB.GetCollection<UserFile>("user_files");
        // Если такой файл уже есть - не надо ничего делать с базой. Поищем...
        var filter = new BsonDocument()
        {
            { "Hash", file.Hash }
        };
        var tFiles = uFiles.Find(filter).ToList();
        if (tFiles != null || tFiles.Count > 0)
            return file.Hash;

        uFiles.InsertOne(file);
        return file.Hash;
    }
    
    /// <summary> Возвращает файл по его хешу </summary>
    [HttpGet(Name = "GetFileByHash")]
    public  IActionResult Get(){
        var request = HttpContext.Request;
        var query = request.Query;
        var hash = query.Where(x => x.Key == "hash").Select(x => x.Value).First().ToString();
        if ( string.IsNullOrEmpty(hash)) {
            var ms = new MemoryStream();
            return File(ms, "application/zip", fileDownloadName: "empty");
        }
        var uFiles = _mongoDB.GetCollection<UserFile>("user_files");
        // Если такой файл уже есть - не надо ничего делать с базой. Поищем...
        var filter = new BsonDocument()
        {
            { "Hash", hash }
        };
        var tFiles = uFiles.Find(filter).ToList();
        if (tFiles != null || tFiles.Count > 0)
            return File(tFiles[0].file, "application/octet-stream", fileDownloadName: tFiles[0].source_name);
        
        // случай, когда по хэшу нет файла        
        var ms1 = new MemoryStream();
        return File(ms1, "application/zip", fileDownloadName: "empty");
    }
    
    
    
}