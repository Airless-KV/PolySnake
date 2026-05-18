using System;
using System.IO;
using UnityEngine;

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
        string docsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
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
                SystemSettings settings = JsonUtility.FromJson<SystemSettings>(json);
                if (settings != null) return settings;
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Failed to load SystemSettings: " + ex.Message);
            }
        }
        return new SystemSettings();
    }

    public void Save()
    {
        string filePath = GetSettingsPath();
        try
        {
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Failed to save SystemSettings: " + ex.Message);
        }
    }
}
