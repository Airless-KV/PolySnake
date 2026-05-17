using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class GameBootManager
{
    public static string CurrentMapName = "TM1_MainTestScene";
    public static float CurrentMapSize = 200f;
    public static string CurrentGameMode = "TimeLimit";
    public static float TimeLimit = 120f;
    public static int TargetSize = 50;
    public static bool IsEndurance = false;
    public static string TimeOfDay = "Day";
    public static float PlayerSpeed = 20f;

    private static bool hasBooted = false;

    [System.Serializable]
    public class GameSettings
    {
        public string MapMode;
        public float MapSize;
        public string GameMode;
        public float TimeLimitSeconds;
        public int TargetSize;
        public bool IsEndurance;
        public string TimeOfDay;
        public float PlayerSpeed;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void AutoBoot()
    {
        if (hasBooted) return;
        hasBooted = true;

        string docsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        string filePath = Path.Combine(docsPath, "SnakeGameSettings.json");

        if (File.Exists(filePath))
        {
            Debug.Log("Found Settings File! Applying custom rules...");
            
            string jsonText = File.ReadAllText(filePath);
            GameSettings loadedSettings = JsonUtility.FromJson<GameSettings>(jsonText);

            CurrentMapName = loadedSettings.MapMode;
            CurrentMapSize = Mathf.Clamp(loadedSettings.MapSize, 25f, 50f);
            CurrentGameMode = loadedSettings.GameMode;
            TimeLimit = loadedSettings.TimeLimitSeconds;
            TargetSize = loadedSettings.TargetSize;
            IsEndurance = loadedSettings.IsEndurance;
            TimeOfDay = string.IsNullOrEmpty(loadedSettings.TimeOfDay) ? "Day" : loadedSettings.TimeOfDay;
            PlayerSpeed = (loadedSettings.PlayerSpeed > 0) ? loadedSettings.PlayerSpeed : 20f;
        }
        else
        {
            Debug.LogWarning("No settings file found. Using default testing rules.");
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (SceneManager.GetActiveScene().name != CurrentMapName)
        {
            Debug.Log("Loading Map Scene: " + CurrentMapName);
            SceneManager.LoadScene(CurrentMapName);
        }
        else
        {
            ApplyRulesToWorld();
        }
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == CurrentMapName)
        {
            ApplyRulesToWorld();
        }
    }

    private static void ApplyRulesToWorld()
    {
        MainApple appleScript = Object.FindAnyObjectByType<MainApple>();
        if (appleScript != null)
        {
            if (CurrentMapName.Contains("Flat")) 
                appleScript.currentMapMode = MainApple.MapMode.Flat2D;
            else 
                appleScript.currentMapMode = MainApple.MapMode.Spherical3D;
                
            appleScript.orbitRadius = CurrentMapSize * 2f; 
            appleScript.flatMapSize = new Vector2(CurrentMapSize, CurrentMapSize);
            appleScript.flatSkyHeight = CurrentMapSize * 2f;

            if (appleScript.geometryCenter != null)
            {
                appleScript.geometryCenter.localScale = Vector3.one * CurrentMapSize;
                Debug.Log("Resized Map visually to: " + CurrentMapSize);
            }

            Physics.SyncTransforms();

            appleScript.RandomizePosition();
        }

        RepositionPlayer(appleScript);

        GameObject referee = new GameObject("GameModeReferee");
        referee.AddComponent<GameModeController>();

        GameObject scoreMgr = new GameObject("ScoreManager");
        scoreMgr.AddComponent<ScoreManager>();

        if (IsEndurance)
        {
            GameObject[] walls = GameObject.FindGameObjectsWithTag("wallCollision_Death");
            foreach (GameObject wall in walls)
            {
                wall.SetActive(false); 
            }
            Debug.Log("Endurance Mode: Disabled Walls.");
        }

        string skyboxName = (TimeOfDay == "Night") ? "FS002_Night_Moonless" : "FS002_Day_Sunless";
        Material skyMat = Resources.Load<Material>(skyboxName);
        if (skyMat != null)
        {
            RenderSettings.skybox = skyMat;
            DynamicGI.UpdateEnvironment();
            Debug.Log($"Successfully applied {skyboxName}!");
        }
        else
        {
            Debug.LogWarning($"Could not find skybox material '{skyboxName}' in any Resources folder. You must move it to a Resources folder for this to work!");
        }
    }

    private static void RepositionPlayer(MainApple appleScript)
    {
        if (appleScript == null || appleScript.geometryCenter == null) return;

        GameObject player = GameObject.FindGameObjectWithTag("snakeHead_Player");
        if (player != null)
        {
            Vector3 startPos = appleScript.geometryCenter.position + (Vector3.up * CurrentMapSize * 2f);
            Vector3 direction = -Vector3.up;

            RaycastHit hit;
            if (Physics.Raycast(startPos, direction, out hit, CurrentMapSize * 4f, appleScript.groundLayer))
            {
                float verticalOffset = 0.6f;
                player.transform.position = hit.point + (hit.normal * verticalOffset);
                
                player.transform.up = hit.normal;

                MainPlayerInputHandler inputHandler = player.GetComponent<MainPlayerInputHandler>();
                if (inputHandler != null)
                {
                    inputHandler.moveSpeed = PlayerSpeed;
                    Debug.Log($"Applied Player Speed: {PlayerSpeed}");
                }
                
                Debug.Log("Successfully repositioned Player to the new planet surface!");
            }
            else
            {
                Debug.LogWarning("RepositionPlayer failed: Raycast could not find the ground layer.");
            }
        }
        else
        {
            Debug.LogWarning("Could not find the player to reposition (looking for tag 'snakeHead_Player').");
        }
    }
}
