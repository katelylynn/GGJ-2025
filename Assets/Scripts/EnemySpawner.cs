using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float spawnInterval = 10.0f;    // Time between spawns
    private float spawnOffset = 0.2f;                       // Controls how far from the screen the enemies spawn
    
    [SerializeField] private int wave = 1;
    [SerializeField] private int totalWaves = 4;
    [SerializeField] private int multiplier = 5;         

    public static event Action EnemiesDefeated;

    private void Start()
    {
        InvokeRepeating("SpawnWave", 0f, spawnInterval);
        Player.PlayerDied += () => { Destroy(gameObject); };
    }

    private void Update()
    {
        if (wave >= totalWaves && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) 
        {
            EnemiesDefeated?.Invoke();
            Destroy(gameObject);
        }
    }

    private void SpawnWave()
    {
        if (wave >= totalWaves)
        {
            CancelInvoke("SpawnWave");
            return;
        }

        Debug.Log("Wave " + wave + ": Spawning " + wave * multiplier + " enemies!");
        for (int i = 0; i < wave * multiplier; i++)
        {
            SpawnEnemy();
        }

        wave++;
    }

    private void SpawnEnemy()
    {
        // Get the camera's bounds in world space
        Camera mainCamera = Camera.main;
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;

        // Randomly choose to spawn the enemy off-screen (left, right, top, or bottom)
        Vector3 spawnPosition = Vector3.zero;

        // Randomly pick a side to spawn off-screen
        int side = UnityEngine.Random.Range(0, 4);  // 0 = left, 1 = right, 2 = top, 3 = bottom

        switch (side)
        {
            case 0:  // Spawn off the left
                spawnPosition = new Vector3(-screenWidth - spawnOffset, UnityEngine.Random.Range(-screenHeight, screenHeight), 0f);
                break;
            case 1:  // Spawn off the right
                spawnPosition = new Vector3(screenWidth + spawnOffset, UnityEngine.Random.Range(-screenHeight, screenHeight), 0f);
                break;
            case 2:  // Spawn off the top
                spawnPosition = new Vector3(UnityEngine.Random.Range(-screenWidth, screenWidth), screenHeight + spawnOffset, 0f);
                break;
            case 3:  // Spawn off the bottom
                spawnPosition = new Vector3(UnityEngine.Random.Range(-screenWidth, screenWidth), -screenHeight - spawnOffset, 0f);
                break;
        }

        // Instantiate the enemy prefab at the off-screen position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
