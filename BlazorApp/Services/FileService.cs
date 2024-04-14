namespace BlazorApp.Services;

public class FileService
{
    private string? Server;
    
    public FileService(IConfiguration Configuration)
    {
        Server = Configuration.GetValue<string>("FileServer");
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