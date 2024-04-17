
namespace WebApp.FileStorageAdapter
{
    public class FileStorageAdapter : IFileStorageAdapter
    {
        public FileStorageAdapter()
        {
        }
        public Task<byte> GetFileById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFile(byte[] array)
        {
            string result;
            using (var httpClient = new HttpClient())
            {
                MultipartFormDataContent form = new MultipartFormDataContent
                {
                    { new StringContent("123"), "userId" },
                    { new ByteArrayContent(array, 0, array.Length), "file", "test" }
                };

                HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7173/api/file", form);
              
                response.EnsureSuccessStatusCode();

                result = response.Content.ReadAsStringAsync().Result;
            }

            return result;
        }
    }
}
