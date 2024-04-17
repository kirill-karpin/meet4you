using WebApp.FileStorageAdapter;
using WebApp.Controllers;

namespace WebApp.Service
{
    class FileService : IFileService
    {
        private readonly IFileStorageAdapter _fileAdapter;

        public FileService(IFileStorageAdapter fileAdapter)
        {
            _fileAdapter = fileAdapter;
        }

        public async Task<byte> GetFileById(string id)
        {
            return await _fileAdapter.GetFileById(id);
        }

        public async Task<string> SaveFile(byte[] array)
        {
            return await _fileAdapter.SaveFile(array);
        }
    }
}
