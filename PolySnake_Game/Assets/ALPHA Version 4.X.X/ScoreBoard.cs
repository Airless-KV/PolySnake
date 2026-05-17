using UnityEngine;
using TMPro;

// NOTE: This is an ALPHA-era leftover. The active ScoreManager is in BETA Version 1.X.X/ScoreManager.cs.
// Renamed to avoid CS0101 duplicate class conflict.
public class ScoreManager_Alpha : MonoBehaviour
{
    public static ScoreManager_Alpha Instance;

    public TextMeshProUGUI scoreText;

    private int score = 0;

    void Awake()
    {
        // Simple singleton (so other scripts can access it)
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}