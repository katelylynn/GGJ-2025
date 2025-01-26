using UnityEngine;

public class ScoreIncrementer : MonoBehaviour
{
    public GameObject scoreText;
    public ScoreManager scoreManager;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var inputPrefab = GetComponent<GameObject>();
        if (other.gameObject.CompareTag(inputPrefab.tag)) IncrementScore();
    }

    private void IncrementScore()
    {
        scoreManager.IncrementScore();
    }
}