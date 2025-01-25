using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBehaviour : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    private Rigidbody2D myRigidBody;
    private int _count = 0;
    private List<GameObject> resourses = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 spawnDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            Vector2 spawnPosition = new Vector2(transform.position.x + spawnDirection.x, transform.position.y + spawnDirection.y);
            
            var instance = Instantiate(myPrefab, spawnPosition, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().velocity += spawnDirection * Random.Range(2f, 3f);
            _count++;
        }
    }
}