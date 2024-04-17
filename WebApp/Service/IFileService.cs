namespace WebApp.Service
{
    public interface IFileService
    {
        public Task<string> SaveFile(Stream stream);

        public Task<byte> GetFileById(string id);

        public Task<byte> RemoveFile(string id);
    }
}
