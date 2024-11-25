using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mother : MonoBehaviour
{
    public Transform player;           
    public float sightRange = 10f;    
    public float fieldOfView = 90f;   
    public float wanderRadius = 15f;   
    public float wanderTime = 3f;      
    private NavMeshAgent agent;
    private float timeSinceLastWander = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
       
        if (PlayerInSight())
        {
            FollowPlayer();
        }
        else
        {
            WanderRandomly();
        }
    }

    bool PlayerInSight()
    {
        
        if (Vector3.Distance(transform.position, player.position) < sightRange)
        {
            
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < fieldOfView / 2)
            {
                // sets raycast to player when within range 
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, out hit, sightRange))
                {
                    if (hit.transform == player)
                    {
                        return true; 
                    }
                }
            }
        }
        return false; 
    }

    void FollowPlayer()
    {
        
        agent.SetDestination(player.position);
    }

    void WanderRandomly()
    {
        
        timeSinceLastWander += Time.deltaTime;
        if (timeSinceLastWander >= wanderTime)
        {
         
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;

            
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
            {
                
                agent.SetDestination(hit.position);
            }

            
            timeSinceLastWander = 0f;
        }
    }
}
