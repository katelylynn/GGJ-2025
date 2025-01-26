using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float damageAmount = 0.1f;

    public float speed = 10f;

    public GameObject canvas;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireRateUpgradeScaling = 1f;
    private Animator animator;
    private int direction;

    private float health = 1.0f;

    private bool isDead;
    private Vector2 lastDirection;
    private bool now;

    private float lastFireTime = 0f;

    private Dictionary<int, Vector2> projectilePositions = new Dictionary<int, Vector2>
    {
        { 1, new Vector2(0.32f, -0.33f) },
        { 2, new Vector2(0.32f, -0.33f) },
        { 3, new Vector2(0.32f, -0.33f) },
        { 4, new Vector2(0.32f, -0.33f) },
        { 5, new Vector2(-0.2f, -0.33f) },
        { 6, new Vector2(-0.37f, -0.33f) },
        { 7, new Vector2(-0.37f, -0.33f) },
        { 8, new Vector2(-0.37f, -0.33f) }
    };

    private void Start()
    {
        animator = GetComponent<Animator>();
        direction = 3;
        lastDirection = new Vector2(1f, 0f);
    }

    private void Update()
    {
        direction = GetDirection();
        var movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movementDirection.magnitude > 0f) lastDirection = movementDirection;
        Animate(direction);
        if (isDead == false) Move(movementDirection);
        if (isDead == false && GameManager.Instance.phase == 2 && Input.GetKey(KeyCode.Space)) {

            if (GameManager.Instance.ownedPowerUps[GameManager.PowerUpType.MULTISHOT] > 0) {
                TripleShoot(direction, lastDirection);
            } else {
                Shoot(direction, lastDirection);
            }

        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if ((health - damageAmount) * Time.deltaTime <= 0 && isDead == false)
            {
                isDead = true;
                animator.SetBool("walking", false);
                animator.SetTrigger("PlayerDied");
                PlayerDied?.Invoke();
                Destroy(gameObject, 3.0f);
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

    private void Move(Vector2 movementDirection)
    {
        var inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        animator.SetBool("walking", movementDirection.magnitude > 0f);
        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);
    }

    private void Shoot(int dir, Vector2 movementDirection)
    {
        float delay = (1f / (1f + GameManager.Instance.ownedPowerUps[GameManager.PowerUpType.FIRERATE] * fireRateUpgradeScaling));
        if (Time.time >= lastFireTime + delay) {
            Debug.Log("Shooting. Current delay set to " + delay + " seconds.");

            lastFireTime = Time.time;

            animator.SetTrigger("Shoot");
            var projectile = Instantiate(projectilePrefab, transform.TransformPoint(projectilePositions[dir]),
                Quaternion.identity);
            var angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void TripleShoot(int dir, Vector2 movementDirection)
    {
        float delay = (1f / (1f + GameManager.Instance.ownedPowerUps[GameManager.PowerUpType.FIRERATE] * fireRateUpgradeScaling));
        
        if (Time.time >= lastFireTime + delay)
        {
            Debug.Log("Shooting. Current delay set to " + delay + " seconds.");
            
            lastFireTime = Time.time;

            // Trigger the shooting animation
            animator.SetTrigger("Shoot");

            // Base angle for the direction
            float baseAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;

            // Cone spread angle
            float spread = 10f; // Adjust for tighter or wider cone
            
            // Create and fire three projectiles
            for (int i = -1; i <= 1; i++) // Loops -1, 0, 1 for the cone
            {
                // Calculate the angle for this projectile
                float angle = baseAngle + (i * spread);

                // Instantiate the projectile
                var projectile = Instantiate(projectilePrefab, transform.TransformPoint(projectilePositions[dir]), Quaternion.identity);

                // Set the rotation of the projectile
                projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }

}
