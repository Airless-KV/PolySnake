using UnityEngine;
using TMPro;

public class MainGameHUDManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI timerText;

    private string gameMode;
    private float  timeLimitSeconds;
    private int    targetSize;

    private float elapsedTime = 0f;

    private float timeRemaining;

    private bool gameEnded = false;

    private int lastTickSecond = -1;

    void Start()
    {
        gameMode         = MainGameBootManager.CurrentGameMode;
        timeLimitSeconds = MainGameBootManager.TimeLimit;
        targetSize       = MainGameBootManager.TargetSize;
        timeRemaining    = timeLimitSeconds;

        if (scoreText == null || timerText == null)
        {
            Debug.LogWarning("[GameHUDManager] Score or Timer Text reference is missing! Please assign them in the Inspector.");
        }
    }

    void Update()
    {
        if (MainGameModeController.Instance != null && MainGameModeController.Instance.IsGameEnded)
        {
            gameEnded = true;
        }

        if (!gameEnded)
        {
            elapsedTime   += Time.deltaTime;
            timeRemaining -= Time.deltaTime;
            timeRemaining  = Mathf.Max(timeRemaining, 0f);

            // Tick sound warning when under 10 seconds in Time Limit mode
            if (gameMode == "Time Limit" && timeRemaining <= 10f && timeRemaining > 0f)
            {
                int currentSecond = Mathf.FloorToInt(timeRemaining);
                if (currentSecond != lastTickSecond)
                {
                    lastTickSecond = currentSecond;
                    if (MainSoundManager.Instance != null)
                    {
                        MainSoundManager.Instance.PlaySFX("WarningTick");
                    }
                }
            }
        }

        UpdateScoreDisplay();
        UpdateTimerDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText == null) return;

        int currentScore = MainGameModeController.Instance != null
            ? MainGameModeController.Instance.GetCurrentScore()
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
