using System.Text.Json;

namespace Task_5.Services
{
    public class FileStore<T>
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        public FileStore(IWebHostEnvironment env, string fileName)
        {
            var dir = Path.Combine(env.ContentRootPath, "App_Data");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            _path = Path.Combine(dir, fileName);
            if (!File.Exists(_path)) File.WriteAllText(_path, "[]");
        }

        public List<T> Load()
            => JsonSerializer.Deserialize<List<T>>(File.ReadAllText(_path)) ?? new List<T>();

        public void Save(List<T> items)
            => File.WriteAllText(_path, JsonSerializer.Serialize(items, _opts));
    }
}
