namespace WebApp.FileStorageAdapter
{
    interface IFileStorageAdapter
    {
        Task<string> SaveFile(byte[] array);
        Task<byte> GetFileById(string id);
    }
}
