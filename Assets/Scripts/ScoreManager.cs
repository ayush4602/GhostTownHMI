using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;  // Singleton instance
    public static int finalScore = 0;  // Static variable to store the final score across scenes
    public Text scoreText;  // UI Text to display the score
    private int score = 0;  // Keep track of the zombie kills

    private void Awake()
    {
        // Ensure there is only one instance of the ScoreManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Make sure the score persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI();  // Initialize the score UI
    }

    // Method to increase the score
    public void AddScore(int value)
    {
        score += value;
        finalScore = score;  // Update the static variable for the final score
        UpdateScoreUI();  // Update the UI when the score changes
    }

    // Update the score UI text
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
