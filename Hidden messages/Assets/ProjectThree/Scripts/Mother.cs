using UnityEngine;
using UnityEngine.AI;

public class Mother : MonoBehaviour
{
    public Transform player;           // Reference to the player
    public float sightRange = 10f;     // The range at which the enemy can see the player
    public float fieldOfView = 90f;    // The angle of vision in degrees
    public float wanderRadius = 15f;   // The radius within which the enemy can wander
    public float wanderTime = 3f;      // Time between random wander movements

    private NavMeshAgent agent;
    private float timeSinceLastWander = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // If player reference is not assigned, find the player by tag
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
        // If the enemy can see the player, follow the player
        if (CanSeePlayer())
        {
            FollowPlayer();
        }
        else
        {
            WanderRandomly();
        }
    }

    bool CanSeePlayer()
    {
        // Check if player is within sight range
        if (Vector3.Distance(transform.position, player.position) < sightRange)
        {
            // Get direction from the enemy to the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Check if the player is within the field of view angle
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < fieldOfView / 2)
            {
                // Raycast to ensure there's no obstacle between the enemy and the player
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, out hit, sightRange))
                {
                    if (hit.transform == player)
                    {
                        return true; // Player is in sight
                    }
                }
            }
        }
        return false; // Player is not in sight
    }

    void FollowPlayer()
    {
        // Set the destination of the NavMeshAgent to the player's position
        agent.SetDestination(player.position);
    }

    void WanderRandomly()
    {
        // If the enemy has wandered long enough, pick a new random destination
        timeSinceLastWander += Time.deltaTime;
        if (timeSinceLastWander >= wanderTime)
        {
            // Get a random position within the wander radius
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;

            // Check if the random point is on the NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
            {
                // Set the NavMeshAgent's destination to the random point
                agent.SetDestination(hit.position);
            }

            // Reset the wander timer
            timeSinceLastWander = 0f;
        }
    }
}
