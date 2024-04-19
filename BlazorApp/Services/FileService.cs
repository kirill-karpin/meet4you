namespace BlazorApp.Services;

public class FileService
{
    private readonly HttpClient _httpClient;
    private string? Server;
    
    public FileService(IConfiguration Configuration, HttpClient _httpClient)
    {
        this._httpClient = _httpClient;
        Server = Configuration.GetValue<string>("FileServer");
    }

    public string GetFileUploadPath()
    {
        return $"{Server}/api/file";
    }

    public async Task<HttpResponseMessage> UploadFile(HttpContent formData)
    {
        Console.WriteLine("UPLOAD");
        var response = await _httpClient.PostAsync(GetFileUploadPath(), formData);
        return response;
    }

    public string GetFilePath(string? id)
    {
        if (!String.IsNullOrEmpty(id))
        {
            return $"{Server}/api/file/{id}";
        }
        return "photo-default.jpg";
    }
}