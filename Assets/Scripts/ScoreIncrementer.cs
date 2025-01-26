using UnityEngine;

public class ScoreIncrementer : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject scoreManager;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var inputPrefab = GetComponent<GameObject>().gameObject;
        if (other.gameObject.CompareTag(inputPrefab.tag) && other.gameObject.transform.position.magnitude > 1000)
            IncrementScore();
        IncrementScore();
    }

    private void IncrementScore()
    {
        scoreManager.GetComponent<ScoreManager>().IncrementScore("resourse");
    }
}