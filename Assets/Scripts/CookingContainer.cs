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
    private bool isActive;

    private void Start()
    {
        Disable();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("this is a collision");

        if (isActive && other.gameObject.CompareTag(inputPrefab.tag) && queue.Count < maxQueueLength)
        {
            queue.Enqueue(other.gameObject);
            // Destroy(other.gameObject); // dont want to destroy so that we can check the tag name
            other.gameObject.transform.position = new Vector3(100, 200, 10000);
            var timerLocation = transform.position + new Vector3(0, .04f * queue.Count, 0);

            var instance = Instantiate(UiPreFab, timerLocation, Quaternion.identity, UiCanvas.transform);
            instance.GetComponent<UITimer>().Max = cookTime;
            instance.GetComponent<UITimer>().time = cookTime;

            StartCoroutine(WaitForCook());
        }
    }

    public void Enable() 
    {
        isActive = true;
    }

    public void Disable() 
    {
        isActive = false;
    }

    public void Toggle() {
        isActive = !isActive;
    }

    private IEnumerator WaitForCook()
    {
        yield return new WaitForSeconds(cookTime);
        var spawner = GetComponent<SpawnerBehaviour>();
        spawner?.Spawn("cooked");
        queue.Dequeue();
    }
}