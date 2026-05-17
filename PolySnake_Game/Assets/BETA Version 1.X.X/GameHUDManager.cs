using UnityEngine;
using TMPro;

public class GameHUDManager : MonoBehaviour
{
    [Header("HUD Text References")]
    [Tooltip("Top-left label: shows score (and goal for Target Size mode).")]
    public TextMeshProUGUI scoreText;

    [Tooltip("Top-right label: shows time remaining or time elapsed.")]
    public TextMeshProUGUI timerText;

    private string gameMode;
    private float  timeLimitSeconds;
    private int    targetSize;

    private float elapsedTime = 0f;

    private float timeRemaining;

    private bool gameEnded = false;

    void Start()
    {
        gameMode         = GameBootManager.CurrentGameMode;
        timeLimitSeconds = GameBootManager.TimeLimit;
        targetSize       = GameBootManager.TargetSize;
        timeRemaining    = timeLimitSeconds;

        if (scoreText == null || timerText == null)
        {
            Debug.LogWarning("[GameHUDManager] Score or Timer Text reference is missing! Please assign them in the Inspector.");
        }
    }

    void Update()
    {
        if (GameModeController.Instance != null && GameModeController.Instance.IsGameEnded)
        {
            gameEnded = true;
        }

        if (!gameEnded)
        {
            elapsedTime   += Time.deltaTime;
            timeRemaining -= Time.deltaTime;
            timeRemaining  = Mathf.Max(timeRemaining, 0f);
        }

        UpdateScoreDisplay();
        UpdateTimerDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText == null) return;

        int currentScore = GameModeController.Instance != null
            ? GameModeController.Instance.GetCurrentScore()
            : 0;

        if (gameMode == "Target Size")
        {
            scoreText.text = $"Score: {currentScore} / {targetSize}";
        }
        else
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText == null) return;

        Color neonGreen = new Color(0.22f, 1f, 0.08f); // RGB: 57, 255, 20
        Color warningRed = Color.red;

        if (gameMode == "Time Limit")
        {
            timerText.text  = FormatTime(timeRemaining);
            timerText.color = timeRemaining <= 10f ? warningRed : neonGreen;
        }
        else
        {
            timerText.text  = FormatTime(elapsedTime);
            timerText.color = neonGreen;
        }
    }

    private string FormatTime(float seconds)
    {
        int m = Mathf.FloorToInt(seconds / 60f);
        int s = Mathf.FloorToInt(seconds % 60f);
        return $"{m:00}:{s:00}";
    }
}
