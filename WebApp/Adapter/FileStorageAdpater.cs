
namespace WebApp.Adapter
{
    public class FileStorageAdpater : IFileStorageAdpater
    {
        public Task<byte> GetFileById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<byte> RemoveFile(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveFile(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
