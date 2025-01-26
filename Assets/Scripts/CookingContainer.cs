using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingContainer : MonoBehaviour
{
    public float cookTime = 3f;
    public GameObject inputPrefab;
    public int maxQueueLength = 5;
    public GameObject UiPreFab;
    public GameObject UiCanvas;
    private readonly Queue<GameObject> queue = new();

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("this is a collision");
        if (other.gameObject.CompareTag(inputPrefab.tag) && queue.Count < maxQueueLength)
        {
            queue.Enqueue(other.gameObject);
            Destroy(other.gameObject);
            var timerLocation = transform.position + new Vector3(0, .04f * queue.Count, 0);

            var instance = Instantiate(UiPreFab, timerLocation, Quaternion.identity, UiCanvas.transform);
            instance.GetComponent<UITimer>().Max = cookTime;
            instance.GetComponent<UITimer>().time = cookTime;

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
}