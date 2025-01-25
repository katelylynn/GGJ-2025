using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBehaviour : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public int count;
    private readonly List<GameObject> resourses = new();
    private Rigidbody2D myRigidBody;

    public void Spawn()
    {
        var spawnDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        var spawnPosition =
            new Vector2(transform.position.x + spawnDirection.x, transform.position.y + spawnDirection.y);

        var instance = Instantiate(myPrefab, spawnPosition, Quaternion.identity);
        resourses.Add(instance);
        instance.GetComponent<Rigidbody2D>().velocity += spawnDirection * Random.Range(2f, 3f);
        count++;
    }
}