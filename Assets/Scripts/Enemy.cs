using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private float speed = 0.2f;
    private float detectionRange = 1f; // Range to check for obstacles
    private float avoidanceStrength = 2f; // How much it steers away from obstacles

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        Player.PlayerDied += () => { Destroy(gameObject); };
    }

    private void Update()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        
        // Check for obstacles in front of the enemy
        if (Physics.Raycast(transform.position, directionToPlayer, detectionRange))
        {
            // If there's an obstacle, steer away from it
            Vector3 avoidanceDirection = Vector3.Cross(directionToPlayer, Vector3.up).normalized;
            directionToPlayer += avoidanceDirection * avoidanceStrength;
        }

        // Move towards the player, while avoiding obstacles
        transform.position = Vector3.MoveTowards(transform.position, transform.position + directionToPlayer, speed * Time.deltaTime);
    }
}
