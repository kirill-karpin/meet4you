using WebApp.Adapter;
using WebApp.Controllers;

namespace WebApp.Service
{
    class FileService : IFileService
    {
        private readonly IFileStorageAdpater _fileAdapter;

        public FileService(IFileStorageAdpater fileAdapter)
        {
            _fileAdapter = fileAdapter;
        }

        public async Task<byte> GetFileById(string id)
        {
            return await _fileAdapter.GetFileById(id);
        }

        public async Task<byte> RemoveFile(string id)
        {
            return await _fileAdapter.RemoveFile(id);
        }

        public async Task<string> SaveFile(Stream stream)
        {
            return await _fileAdapter.SaveFile(stream);
        }
    }
}
