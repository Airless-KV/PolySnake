using UnityEngine;
using System.IO;

public class GameModeController : MonoBehaviour
{
    public static GameModeController Instance;

    private string currentMode;
    private float timeLeft;
    private int targetSize;
    private MainSnakeTailHandler tailHandler;
    private bool gameEnded = false;

    public bool IsGameEnded => gameEnded;
    public float TimeRemaining => timeLeft;

    [System.Serializable]
    public class GameResults
    {
        public int FinalScore;
        public string Reason;
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentMode = GameBootManager.CurrentGameMode;
        timeLeft = GameBootManager.TimeLimit;
        targetSize = GameBootManager.TargetSize;

        GameObject player = GameObject.FindGameObjectWithTag("snakeHead_Player");
        if (player != null)
        {
            tailHandler = player.GetComponent<MainSnakeTailHandler>();
        }

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (gameEnded) return;

        if (currentMode == "Time Limit")
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                EndGame("Time's Up!");
            }
        }

        if (currentMode == "Target Size" && tailHandler != null)
        {
            int currentScore = GetCurrentScore();
            if (currentScore >= targetSize)
            {
                EndGame("Target Size Reached!");
            }
        }
    }

    public int GetCurrentScore()
    {
        if (tailHandler != null)
        {
            return tailHandler.GetTailCount();
        }
        return 0;
    }

    public void EndGame(string reason)
    {
        if (gameEnded) return;
        gameEnded = true;

        Time.timeScale = 0f;

        GameResults results = new GameResults
        {
            FinalScore = GetCurrentScore(),
            Reason = reason
        };

        string docsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        string filePath = Path.Combine(docsPath, "SnakeGameResults.json");
        
        string jsonText = JsonUtility.ToJson(results);
        File.WriteAllText(filePath, jsonText);

        Debug.Log($"Game Over! Reason: {reason}. Score: {results.FinalScore}. Saved to {filePath}");
    }
}
