namespace WebApp.Adapter
{
    interface IFileStorageAdpater
    {
        Task<string> SaveFile(Stream stream);
        Task<byte> GetFileById(string id);
        Task<byte> RemoveFile(string id);
    }
}
