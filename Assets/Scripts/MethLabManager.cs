using System.Collections.Generic;
using UnityEngine;

public class MethLabManager : MonoBehaviour
{
    public int numLabs;
    public GameObject labPrefab;
    public float spawnInterval = 2f; // Time interval in seconds
    private readonly int methLabMaxResourses = 5;
    private List<GameObject> _methLabs;
    private float _timer; // Tracks time

    // Start is called before the first frame update
    private void Start()
    {
        CreateMethLabs();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            _timer = 0f; // Reset the timer

            foreach (var methLab in _methLabs)
            {
                if (methLab != null)
                    if (methLab.GetComponent<SpawnerBehaviour>()?.count < methLabMaxResourses)
                        methLab.GetComponent<SpawnerBehaviour>()
                            ?.Spawn();
                Debug.Log(methLab);
            }
        }
    }


    private void CreateMethLabs()
    {
        _methLabs = new List<GameObject>();

        for (var i = 0; i < numLabs; i++)
        {
            var spawnPoint = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
            var methLab = Instantiate(labPrefab, spawnPoint, Quaternion.identity);
            _methLabs.Add(methLab);
        }
    }
}