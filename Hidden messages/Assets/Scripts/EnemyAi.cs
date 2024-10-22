using UnityEngine;

public class TurtleAI : MonoBehaviour
{
    public Transform pointA; // Starting point for the turtle's movement
    public Transform pointB; // End point for the turtle's movement
    public float moveSpeed = 2f; // Speed of the turtle
    public float attackRange = 5f; // Distance to detect the player
    public float attackCooldown = 1f; // Time between attacks
    public GameObject player; // Reference to the player
    private PlayerHealth playerHelath;
    private bool movingToB = true; // Direction of movement
    private float lastAttackTime;

    private void Update()
    {
        if (IsPlayerInRange())
        {
            MoveTowardsPlayer();
            Attack();
        }
        else
        {
            MoveBackAndForth();
        }
    }

    private void MoveBackAndForth()
    {
        // Move the turtle back and forth between point A and B
        Vector3 targetPosition = movingToB ? pointB.position : pointA.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the turtle reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingToB = !movingToB; // Switch direction
        }
    }

    private bool IsPlayerInRange()
    {
        // Check if the player is within attack range
        return Vector3.Distance(transform.position, player.transform.position) < attackRange;
    }

    private void MoveTowardsPlayer()
    {
        // Move towards the player's position
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Example attack logic
            Debug.Log("Turtle attacks the player!");
            lastAttackTime = Time.time;

            // Find the player GameObject
            GameObject playerObject = GameObject.Find("Player");

            // Ensure the player exists
            if (playerObject != null)
            {
                // Get the player's health component
                PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();

                // Ensure the health component exists
                if (playerHealth != null)
                {
                    // Apply damage to the player
                    playerHealth.TakeDamage(1); // Assuming TakeDamage method exists
                }
                else
                {
                    Debug.LogError("PlayerHealth component not found on Player.");
                }
            }
            else
            {
                Debug.LogError("Player GameObject not found.");
            }
        }
    }


    private void OnDrawGizmos()
    {
        // Visualize the attack range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
