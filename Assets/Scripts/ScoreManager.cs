using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}