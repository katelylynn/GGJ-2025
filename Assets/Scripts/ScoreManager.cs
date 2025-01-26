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
                GameManager.Instance.inventory[0] += 10;
                break;
            case "CookedResource":
                GameManager.Instance.inventory[0] += 40;
                break;
            case "Refined":
                GameManager.Instance.inventory[0] += 90;
                break;
            default:
                Debug.LogWarning("Unknown tag: " + tag);
                return;
        }
    }

    public int GetScore()
    {
        return score;
    }
}