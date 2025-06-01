// Services/SettingsService.cs
using System.Text.Json;
using Task_6.Models;

namespace Task_6.Services
{
    public class SettingsService
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        public SettingsService(IWebHostEnvironment env)
        {
            var dir = Path.Combine(env.ContentRootPath, "App_Data");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            _path = Path.Combine(dir, "settings.json");

            // Якщо немає — створити з дефолтом 3 спроби
            if (!File.Exists(_path))
            {
                var def = new TestSettings { AllowedAttempts = 3 };
                File.WriteAllText(_path, JsonSerializer.Serialize(def, _opts));
            }
        }

        public TestSettings Load()
            => JsonSerializer.Deserialize<TestSettings>(File.ReadAllText(_path))!;

        public void Save(TestSettings s)
            => File.WriteAllText(_path, JsonSerializer.Serialize(s, _opts));
    }
}
