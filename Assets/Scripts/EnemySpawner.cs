using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float spawnInterval = 2.0f;    // Time between spawns
    private float spawnOffset = 0.2f;                       // Controls how far from the screen the enemies spawn
    
    private int enemiesSpawned = 0;                         // Counter for number of enemies spawned
    [SerializeField] private int maxEnemies = 20;           // Maximum number of enemies to spawn

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (enemiesSpawned >= maxEnemies)  // Stop spawning after 20 enemies
        {
            CancelInvoke("SpawnEnemy");   // Stop repeating the spawn method
            return;
        }

        // Get the camera's bounds in world space
        Camera mainCamera = Camera.main;
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;

        // Randomly choose to spawn the enemy off-screen (left, right, top, or bottom)
        Vector3 spawnPosition = Vector3.zero;

        // Randomly pick a side to spawn off-screen
        int side = Random.Range(0, 4);  // 0 = left, 1 = right, 2 = top, 3 = bottom

        switch (side)
        {
            case 0:  // Spawn off the left
                spawnPosition = new Vector3(-screenWidth - spawnOffset, Random.Range(-screenHeight, screenHeight), 0f);
                break;
            case 1:  // Spawn off the right
                spawnPosition = new Vector3(screenWidth + spawnOffset, Random.Range(-screenHeight, screenHeight), 0f);
                break;
            case 2:  // Spawn off the top
                spawnPosition = new Vector3(Random.Range(-screenWidth, screenWidth), screenHeight + spawnOffset, 0f);
                break;
            case 3:  // Spawn off the bottom
                spawnPosition = new Vector3(Random.Range(-screenWidth, screenWidth), -screenHeight - spawnOffset, 0f);
                break;
        }

        // Instantiate the enemy prefab at the off-screen position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        enemiesSpawned++;  // Increment the enemy counter
    }
}
