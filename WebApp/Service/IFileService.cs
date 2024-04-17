namespace WebApp.Service
{
    public interface IFileService
    {
        Task<string> SaveFile(byte[] array);

        Task<byte> GetFileById(string id);
    }
}
