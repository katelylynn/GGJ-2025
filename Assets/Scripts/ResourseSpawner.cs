using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBehaviour : MonoBehaviour
{
    public GameObject outputPrefab;
    public int count;
    private readonly List<GameObject> resourses = new();
    private Rigidbody2D myRigidBody;

    public void Spawn(string tagName)
    {
        var bounds = GetComponent<BoxCollider2D>().bounds.extents + new Vector3(.2f, .2f, .2f);

        var spawnDirection = new Vector2(Random.Range(-bounds.x, bounds.x), Random.Range(-bounds.y, bounds.y));
        var spawnPosition =
            new Vector2(transform.position.x + spawnDirection.x, transform.position.y + spawnDirection.y);

        var instance = Instantiate(outputPrefab, spawnPosition, Quaternion.identity);
        resourses.Add(instance);
        instance.GetComponent<Rigidbody2D>().velocity += spawnDirection * Random.Range(2f, 3f);
        count++;
    }
}