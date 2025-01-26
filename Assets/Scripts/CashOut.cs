using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashOut : MonoBehaviour
{
    public float cookTime = 3f;
    public GameObject inputPrefab;
    public int maxQueueLength = 5;
    public GameObject UiPreFab;
    public GameObject UiCanvas;
    public GameObject scoreManager;

    private readonly List<string> acceptedTags = new() { "Refined", "Resourse", "CookedResource" };
    private readonly Queue<GameObject> queue = new();

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (acceptedTags.Contains(other.gameObject.tag) && queue.Count < maxQueueLength)
        {
            queue.Enqueue(other.gameObject);
            // Destroy(other.gameObject); // dont want to destroy so that we can check the tag name
            other.gameObject.transform.position = new Vector3(100, 200, 10000);
            var timerLocation = transform.position + new Vector3(0, .04f * queue.Count, 0);

            var instance = Instantiate(UiPreFab, timerLocation, Quaternion.identity, UiCanvas.transform);
            instance.GetComponent<UITimer>().Max = cookTime;
            instance.GetComponent<UITimer>().time = cookTime;

            var tag = other.gameObject.tag;
            HandleTag(tag);

            scoreManager.GetComponent<ScoreManager>().IncrementScore(tag);
            Destroy(other.gameObject); // dont want to destroy so that we can check the tag name
            StartCoroutine(WaitForCook());
        }
    }

    private IEnumerator WaitForCook()
    {
        yield return new WaitForSeconds(cookTime);
        var spawner = GetComponent<SpawnerBehaviour>();
        spawner?.Spawn("cooked");
        queue.Dequeue();
    }

    public void HandleTag(string tag)
    {
        switch (tag)
        {
            case "Refined":
                GrowBubble(.019f);
                break;

            case "Resourse":
                GrowBubble(.012f);
                break;

            case "CookedResource":
                GrowBubble(.015f);
                break;
        }
    }

    private void GrowBubble(float factor)
    {
        transform.localScale += new Vector3(factor, factor, factor);
    }
}