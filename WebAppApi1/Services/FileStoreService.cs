namespace WebAppApi1.Services
{
    public class FileStoreService
    {
        const string baseDirectory = "c:/demo/";
        public string ReadFile(string fileName)
        {
            return File.ReadAllText($"{baseDirectory}{fileName}");
        }
        public async Task<string> ReadFileAsync(string fileName) 
        {
            return await File.ReadAllTextAsync($"{baseDirectory}{fileName}");
        }
        public void SaveFile(string fileName, string text) 
        {
            File.WriteAllText($"{baseDirectory}{fileName}", text);
        }
    }
}
