using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;


    public void IncrementScore(string tag)
    {
        // Update score based on the tag
        switch (tag)
        {
            case "Resourse":
                score += 1;
                break;
            case "CookedResource":
                score += 4;
                break;
            case "Refined":
                score += 9;
                break;
            default:
                Debug.LogWarning("Unknown tag: " + tag);
                return;
        }

        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}