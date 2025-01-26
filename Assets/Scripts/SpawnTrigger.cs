using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public float spawnInterval = 2f; // Time interval in seconds
    private float _timer; // Tracks time

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            _timer = 0f; // Reset the timer

            GetComponent<SpawnerBehaviour>()
                ?.Spawn("Uncooked");
        }
    }
}