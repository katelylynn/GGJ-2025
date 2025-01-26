using System.Collections;
using UnityEngine;

public class CookingContainer : MonoBehaviour
{
    public float cookTime = 3f;
    public GameObject inputPrefab;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("this is a collision");
        if (other.gameObject.CompareTag(inputPrefab.tag))
        {
            Destroy(other.gameObject);

            // wait here show a circle timer
            // wait for s seconds
            StartCoroutine(WaitForCook());
        }
    }

    private IEnumerator WaitForCook()
    {
        yield return new WaitForSeconds(cookTime);
        var spawner = GetComponent<SpawnerBehaviour>();
        spawner?.Spawn("cooked");
    }
}