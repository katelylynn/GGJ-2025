using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; 
    float speedX, speedY;
    private Rigidbody2D rb;
    public float rotationSpeed = 100f; // Speed of rotation
    public GameObject objectToSpawn; // Reference to the prefab
    public Transform spawnPoint; 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);

        if (movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    void SpawnObject()
    {
        // If a spawn point is set, use its position; otherwise, use the player's position
        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;
        
        // Instantiate the object
        GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Add force (optional)
        Rigidbody2D newRb = newObject.GetComponent<Rigidbody2D>();
        if (newRb != null)
        {
            newRb.AddForce(transform.up * 5f, ForceMode2D.Impulse); // Example force
        }
    } 
}