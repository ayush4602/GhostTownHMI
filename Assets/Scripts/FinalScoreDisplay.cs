using UnityEngine;
using UnityEngine.UI;

public class FinalScoreDisplay : MonoBehaviour
{
    public Text finalScoreText;  // Reference to the Text component in the Canvas

    private void Start()
    {
        // Display the final score when Scene 3 starts
        finalScoreText.text = "Final Score: " + ScoreManager.finalScore.ToString();
    }
}