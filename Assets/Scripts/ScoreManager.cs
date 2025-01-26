using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public int score;

    public void Start()
    {
        Instance = this;
    }

    public static ScoreManager GetInstance()
    {
        if (Instance != null) return Instance;
        throw new Exception("ScoreManager is null");
    }

    public void IncrementScore(string tag)
    {
        // Update score based on the tag
        switch (tag)
        {
            case "Resourse":
                score += 10;
                break;
            case "CookedResource":
                score += 40;
                break;
            case "Refined":
                score += 90;
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