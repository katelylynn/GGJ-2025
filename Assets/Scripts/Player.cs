using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; 
    public float rotationSpeed = 100f;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Move(new Vector2(horizontalInput, verticalInput));
    }

    private void Move(Vector2 movementDirection)
    {
        // identical to enemy move method
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);

        if (movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}