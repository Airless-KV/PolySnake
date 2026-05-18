using System;
using System.IO;
using System.Text.Json;

namespace PolySnake_Launcher
{
    [Serializable]
    public class SystemSettings
    {
        public string Resolution = "1280x720";
        public int FrameRateLimit = 60;
        public bool Fullscreen = false;
        public float MasterVolume = 1.0f;
        public float MusicVolume = 0.8f;
        public float SFXVolume = 0.8f;

        public static string GetSettingsPath()
        {
            string docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(docsPath, "SnakeSystemSettings.json");
        }

        public static SystemSettings Load()
        {
            string filePath = GetSettingsPath();
            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    var options = new JsonSerializerOptions { IncludeFields = true };
                    var settings = JsonSerializer.Deserialize<SystemSettings>(json, options);
                    if (settings != null) return settings;
                }
                catch { }
            }
            return new SystemSettings();
        }

        public void Save()
        {
            string filePath = GetSettingsPath();
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
                string json = JsonSerializer.Serialize(this, options);
                File.WriteAllText(filePath, json);
            }
            catch { }
        }
    }
}
