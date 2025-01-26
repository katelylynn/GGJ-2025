using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;

    private float health = 1.0f;
    [SerializeField] public static float damageAmount = 0.1f;
    public static event Action DamageTaken;
    public static event Action PlayerDied;

    public float speed = 10f; 
    private int direction;
    Vector2 lastDirection;

    private Dictionary<int, Vector2> projectilePositions = new Dictionary<int, Vector2>
    {
        {1, new Vector2(0.32f, -0.33f)},
        {2, new Vector2(0.32f, -0.33f)},
        {3, new Vector2(0.32f, -0.33f)},
        {4, new Vector2(0.32f, -0.33f)},
        {5, new Vector2(-0.2f, -0.33f)},
        {6, new Vector2(-0.37f, -0.33f)},
        {7, new Vector2(-0.37f, -0.33f)},
        {8, new Vector2(-0.37f, -0.33f)},
    };
    [SerializeField] private GameObject projectilePrefab;

    private void Start()
    {
        animator = GetComponent<Animator>();
        direction = 3;
        lastDirection = new Vector2(1f, 0f);
    }

    private void Update()
    {
        direction = GetDirection();
        Vector2 movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movementDirection.magnitude > 0f) lastDirection = movementDirection;
        Animate(direction);
        Move(movementDirection);
        if (Input.GetKeyDown(KeyCode.Space)) Shoot(direction, lastDirection);
    }

    private int GetDirection()
    {
        bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (up && right) return 2;  // NE
        if (up && left) return 8;   // NW
        if (down && right) return 4; // SE
        if (down && left) return 6;  // SW
        if (up) return 1;           // N
        if (right) return 3;        // E
        if (down) return 5;         // S
        if (left) return 7;         // W
        return direction;           // use previous direction
    }

    private void Animate(int dir)
    {
        animator.SetInteger("direction", dir);
    }

    private void Move(Vector2 movementDirection)
    {
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        animator.SetBool("walking", movementDirection.magnitude > 0f);
        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);
    }

    private void Shoot(int dir, Vector2 movementDirection)
    {
        animator.SetTrigger("Shoot");
        GameObject projectile = Instantiate(projectilePrefab, transform.TransformPoint(projectilePositions[dir]), Quaternion.identity);
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy") 
        {
            if ((health - damageAmount) * Time.deltaTime <= 0) PlayerDied?.Invoke();
            else 
            {
                health -= damageAmount * Time.deltaTime;
                DamageTaken?.Invoke();
            }
        }
    }
}