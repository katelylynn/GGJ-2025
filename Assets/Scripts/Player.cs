using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float damageAmount = 0.1f;

    public float speed = 10f;
    public float rotationSpeed = 100f;

    public GameObject prefab;
    public GameObject canvas;
    private Animator animator;
    private int direction;

    private float health = 1.0f;
    private bool now;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        direction = GetDirection();
        Debug.Log(direction);
        Animate(direction);
        if (Input.GetKeyDown(KeyCode.Space))
            Instantiate(prefab, transform.position, Quaternion.identity, canvas.transform);

        Move();
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if ((health - damageAmount) * Time.deltaTime <= 0)
            {
                PlayerDied?.Invoke();
            }
            else
            {
                health -= damageAmount * Time.deltaTime;
                DamageTaken?.Invoke();
            }
        }
    }

    public static event Action DamageTaken;
    public static event Action PlayerDied;

    private int GetDirection()
    {
        var up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        var down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        var left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        var right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if (up && right) return 2; // NE
        if (up && left) return 8; // NW
        if (down && right) return 4; // SE
        if (down && left) return 6; // SW
        if (up) return 1; // N
        if (right) return 3; // E
        if (down) return 5; // S
        if (left) return 7; // W
        return direction; // use previous direction
    }

    private void Animate(int dir)
    {
        animator.SetInteger("direction", dir);
    }

    private void Move()
    {
        var movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        var inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        Debug.Log(animator.GetBool("walking"));
        animator.SetBool("walking", movementDirection.magnitude > 0f);
        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);
    }
}