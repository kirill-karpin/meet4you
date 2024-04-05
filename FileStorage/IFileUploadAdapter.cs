using FileStorage.BO;

namespace FileStorage;

public interface IFileUploadAdapter
{
    public Task<string> SaveFileAsync(UserFile file);
    
    public Task<bool> RemoveFileByIdAsync(string Id);
    public Task<UserFile> GetFileByIdAsync(string Id);
}