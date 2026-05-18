using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class MainInGameSettings : MonoBehaviour
{
    public static MainInGameSettings Instance;

    private bool isMenuOpen = false;
    private SystemSettings settings;

    // Available values
    private string[] resolutions = { "1920x1080", "1280x720", "2560x1440", "1600x900", "1024x768" };
    private int selectedResIndex = 1; // Default to 1280x720

    private string[] fpsLimits = { "30 FPS", "60 FPS", "120 FPS", "144 FPS", "Unlimited" };
    private int selectedFpsIndex = 1; // Default to 60 FPS

    private float masterVol = 1.0f;
    private float musicVol = 0.8f;
    private float sfxVol = 0.8f;
    private bool fullscreen = false;

    // GUI Textures & Styles
    private Texture2D overlayBgTex;
    private Texture2D modalBgTex;
    private Texture2D buttonNormalTex;
    private Texture2D buttonHoverTex;

    private GUIStyle titleStyle;
    private GUIStyle headerStyle;
    private GUIStyle labelStyle;
    private GUIStyle valStyle;
    private GUIStyle btnStyle;
    private GUIStyle cycleBtnStyle;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Load settings initially
        settings = SystemSettings.Load();
        ApplyLoadedSettings();
        InitGUIAssets();
    }

    void Update()
    {
        // Listen for ESC key using new Input System, with fallback to legacy Input
        bool escPressed = false;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                escPressed = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                escPressed = true;
            }
        }

        if (escPressed)
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen)
        {
            // Pause the game
            Time.timeScale = 0f;
            // Load latest settings from file in case launcher changed them
            settings = SystemSettings.Load();
            ApplyLoadedSettings();
        }
        else
        {
            // Unpause the game
            Time.timeScale = 1f;
            SaveAndApplySettings();
        }
    }

    private void ApplyLoadedSettings()
    {
        if (settings == null) settings = new SystemSettings();

        // 1. Resolution
        selectedResIndex = System.Array.IndexOf(resolutions, settings.Resolution);
        if (selectedResIndex < 0) selectedResIndex = 1; // Default 1280x720

        // 2. FPS Limit
        string fpsString = settings.FrameRateLimit switch
        {
            30 => "30 FPS",
            60 => "60 FPS",
            120 => "120 FPS",
            144 => "144 FPS",
            _ => "Unlimited"
        };
        selectedFpsIndex = System.Array.IndexOf(fpsLimits, fpsString);
        if (selectedFpsIndex < 0) selectedFpsIndex = 1; // Default 60 FPS

        // 3. Volumes
        masterVol = Mathf.Clamp01(settings.MasterVolume);
        musicVol  = Mathf.Clamp01(settings.MusicVolume);
        sfxVol    = Mathf.Clamp01(settings.SFXVolume);

        // 4. Fullscreen
        fullscreen = settings.Fullscreen;

        ApplyEngineSettings();
    }

    private void ApplyEngineSettings()
    {
        // 1. Apply Resolution
        string[] resParts = resolutions[selectedResIndex].Split('x');
        if (resParts.Length == 2 && int.TryParse(resParts[0], out int w) && int.TryParse(resParts[1], out int h))
        {
            Screen.SetResolution(w, h, fullscreen);
        }

        // 2. Apply FPS Limit
        int fpsVal = fpsLimits[selectedFpsIndex] switch
        {
            "30 FPS" => 30,
            "60 FPS" => 60,
            "120 FPS" => 120,
            "144 FPS" => 144,
            _ => -1
        };
        Application.targetFrameRate = fpsVal;

        // 3. Apply Audio
        if (MainSoundManager.Instance != null)
        {
            MainSoundManager.Instance.SetVolumes(masterVol, musicVol, sfxVol);
        }
    }

    private void SaveAndApplySettings()
    {
        settings.Resolution = resolutions[selectedResIndex];
        settings.FrameRateLimit = fpsLimits[selectedFpsIndex] switch
        {
            "30 FPS" => 30,
            "60 FPS" => 60,
            "120 FPS" => 120,
            "144 FPS" => 144,
            _ => -1
        };
        settings.MasterVolume = masterVol;
        settings.MusicVolume = musicVol;
        settings.SFXVolume = sfxVol;
        settings.Fullscreen = fullscreen;

        settings.Save();
        ApplyEngineSettings();
    }

    private void InitGUIAssets()
    {
        // Full screen backdrop overlay (semi-transparent dark)
        overlayBgTex = CreateColorTexture(new Color(0.08f, 0.08f, 0.1f, 0.85f));
        // Centered modal box background (solid sleek dark grey)
        modalBgTex   = CreateColorTexture(new Color(0.2f, 0.2f, 0.22f, 0.98f));
        
        // Button textures
        buttonNormalTex = CreateColorTexture(new Color(0.25f, 0.25f, 0.28f, 1f));
        buttonHoverTex  = CreateColorTexture(new Color(0.35f, 0.35f, 0.38f, 1f));
    }

    private Texture2D CreateColorTexture(Color color)
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, color);
        tex.Apply();
        return tex;
    }

    private void SetupStyles()
    {
        if (titleStyle == null)
        {
            titleStyle = new GUIStyle();
            titleStyle.fontSize = 28;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.normal.textColor = Color.white;
            titleStyle.alignment = TextAnchor.MiddleCenter;

            headerStyle = new GUIStyle();
            headerStyle.fontSize = 18;
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = new Color(0f, 0.48f, 0.8f, 1f); // Accent blue
            headerStyle.margin = new RectOffset(0, 0, 10, 5);

            labelStyle = new GUIStyle();
            labelStyle.fontSize = 13;
            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.MiddleLeft;

            valStyle = new GUIStyle();
            valStyle.fontSize = 13;
            valStyle.fontStyle = FontStyle.Bold;
            valStyle.normal.textColor = Color.white;
            valStyle.alignment = TextAnchor.MiddleCenter;

            btnStyle = new GUIStyle();
            btnStyle.fontSize = 14;
            btnStyle.fontStyle = FontStyle.Bold;
            btnStyle.normal.textColor = Color.white;
            btnStyle.normal.background = buttonNormalTex;
            btnStyle.hover.background = buttonHoverTex;
            btnStyle.alignment = TextAnchor.MiddleCenter;

            cycleBtnStyle = new GUIStyle(btnStyle);
            cycleBtnStyle.fontSize = 12;
        }
    }

    void OnGUI()
    {
        if (!isMenuOpen) return;

        SetupStyles();

        // 1. Draw fullscreen overlay
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlayBgTex);

        // 2. Draw centered modal box
        float boxW = 500f;
        float boxH = 550f;
        float boxX = (Screen.width - boxW) / 2f;
        float boxY = (Screen.height - boxH) / 2f;
        
        Rect modalRect = new Rect(boxX, boxY, boxW, boxH);
        GUI.DrawTexture(modalRect, modalBgTex);

        // Padding within the box
        float px = boxX + 30f;
        float py = boxY + 25f;
        float itemW = boxW - 60f;

        // Title
        GUI.Label(new Rect(px, py, itemW, 40f), "SYSTEM SETTINGS", titleStyle);
        py += 55f;

        // SECTION: Video Settings
        GUI.Label(new Rect(px, py, itemW, 25f), "Display / Video", headerStyle);
        py += 30f;

        // Resolution selector row
        GUI.Label(new Rect(px, py, 120f, 25f), "Resolution", labelStyle);
        if (GUI.Button(new Rect(px + 180f, py, 30f, 25f), "<", cycleBtnStyle))
        {
            selectedResIndex = (selectedResIndex - 1 + resolutions.Length) % resolutions.Length;
            ApplyEngineSettings();
        }
        GUI.Label(new Rect(px + 215f, py, 150f, 25f), resolutions[selectedResIndex], valStyle);
        if (GUI.Button(new Rect(px + 370f, py, 30f, 25f), ">", cycleBtnStyle))
        {
            selectedResIndex = (selectedResIndex + 1) % resolutions.Length;
            ApplyEngineSettings();
        }
        py += 35f;

        // Frame rate limit row
        GUI.Label(new Rect(px, py, 120f, 25f), "Frame Rate Limit", labelStyle);
        if (GUI.Button(new Rect(px + 180f, py, 30f, 25f), "<", cycleBtnStyle))
        {
            selectedFpsIndex = (selectedFpsIndex - 1 + fpsLimits.Length) % fpsLimits.Length;
            ApplyEngineSettings();
        }
        GUI.Label(new Rect(px + 215f, py, 150f, 25f), fpsLimits[selectedFpsIndex], valStyle);
        if (GUI.Button(new Rect(px + 370f, py, 30f, 25f), ">", cycleBtnStyle))
        {
            selectedFpsIndex = (selectedFpsIndex + 1) % fpsLimits.Length;
            ApplyEngineSettings();
        }
        py += 45f;

        // Fullscreen Selector Row
        GUI.Label(new Rect(px, py, 120f, 25f), "Fullscreen", labelStyle);
        if (GUI.Button(new Rect(px + 180f, py, 30f, 25f), "<", cycleBtnStyle))
        {
            fullscreen = !fullscreen;
            ApplyEngineSettings();
        }
        GUI.Label(new Rect(px + 215f, py, 150f, 25f), fullscreen ? "Yes" : "No", valStyle);
        if (GUI.Button(new Rect(px + 370f, py, 30f, 25f), ">", cycleBtnStyle))
        {
            fullscreen = !fullscreen;
            ApplyEngineSettings();
        }
        py += 40f;

        // SECTION: Audio Settings
        GUI.Label(new Rect(px, py, itemW, 25f), "Audio / Volume", headerStyle);
        py += 30f;

        // Master Volume slider
        GUI.Label(new Rect(px, py, 150f, 20f), "Master Volume", labelStyle);
        float newMaster = GUI.HorizontalSlider(new Rect(px + 180f, py + 5f, 200f, 20f), masterVol, 0f, 1f);
        GUI.Label(new Rect(px + 390f, py, 50f, 20f), $"{(int)(masterVol * 100)}%", valStyle);
        if (newMaster != masterVol)
        {
            masterVol = newMaster;
            ApplyEngineSettings();
        }
        py += 35f;

        // Music / BGM Volume slider
        GUI.Label(new Rect(px, py, 150f, 20f), "Music / BGM Volume", labelStyle);
        float newMusic = GUI.HorizontalSlider(new Rect(px + 180f, py + 5f, 200f, 20f), musicVol, 0f, 1f);
        GUI.Label(new Rect(px + 390f, py, 50f, 20f), $"{(int)(musicVol * 100)}%", valStyle);
        if (newMusic != musicVol)
        {
            musicVol = newMusic;
            ApplyEngineSettings();
        }
        py += 35f;

        // SFX Volume slider
        GUI.Label(new Rect(px, py, 150f, 20f), "SFX Volume", labelStyle);
        float newSFX = GUI.HorizontalSlider(new Rect(px + 180f, py + 5f, 200f, 20f), sfxVol, 0f, 1f);
        GUI.Label(new Rect(px + 390f, py, 50f, 20f), $"{(int)(sfxVol * 100)}%", valStyle);
        if (newSFX != sfxVol)
        {
            sfxVol = newSFX;
            ApplyEngineSettings();
        }
        py += 60f;

        // Action Buttons
        float btnW = 200f;
        float btnH = 40f;
        
        // Save & Close
        if (GUI.Button(new Rect(boxX + 40f, py, btnW, btnH), "✔  SAVE & RESUME", btnStyle))
        {
            SaveAndApplySettings();
            ToggleMenu();
        }

        // Quit to Launcher
        if (GUI.Button(new Rect(boxX + boxW - 240f, py, btnW, btnH), "✖  QUIT GAME", btnStyle))
        {
            SaveAndApplySettings();
            Time.timeScale = 1f;
            Debug.Log("Exiting game back to launcher...");
            Application.Quit();
        }
    }
}
