using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;

    public float speed = 10f; 
    public float rotationSpeed = 100f;
    private int direction;

    private float health = 1.0f;
    [SerializeField] public static float damageAmount = 0.1f;
    public static event Action DamageTaken;
    public static event Action PlayerDied;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        direction = GetDirection();
        Debug.Log(direction);
        Animate(direction);
        Move();
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

    private void Move()
    {
        Vector2 movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        Debug.Log(animator.GetBool("walking"));
        animator.SetBool("walking", movementDirection.magnitude > 0f);
        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);
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