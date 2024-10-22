using UnityEngine;
using System.Collections.Generic;

public class FallingObject : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign the prefab in the inspector
    public Vector3 spawnPosition = new Vector3(0, 10, 0); // Spawn position in the air
    public Vector3 spawnRotation; // Rotation for the spawned object
    public Transform pointA; // Assign the first target point
    public Transform pointB; // Assign the second target point
    public Transform pointC; // Assign the third target point
    public float moveSpeed = 5f; // Speed of movement
    public float spawnTimer = 2f; // Time between spawns
    public int maxSpawnCount = 8; // Maximum number of objects that can be spawned

    private float spawnTime; // Timer to track spawn time
    private List<GameObject> currentSpawnedObjects = new List<GameObject>(); // Track currently spawned objects
    private List<int> currentTargetIndexes = new List<int>(); // Track the current target index for each object

    void Start()
    {
        spawnTime = spawnTimer; // Initialize the spawn time
    }

    void Update()
    {
        spawnTime -= Time.deltaTime; // Decrease the timer

        // Spawn new objects if the timer reaches zero and current count is less than max
        if (spawnTime <= 0 && currentSpawnedObjects.Count < maxSpawnCount)
        {
            SpawnObject();
            spawnTime = spawnTimer; // Reset the timer
        }

        // Move each spawned object between points
        for (int i = currentSpawnedObjects.Count - 1; i >= 0; i--)
        {
            if (currentSpawnedObjects[i] != null)
            {
                MoveBetweenPoints(currentSpawnedObjects[i], i);
            }
            else
            {
                // Remove destroyed objects from the lists
                currentSpawnedObjects.RemoveAt(i);
                currentTargetIndexes.RemoveAt(i);
            }
        }
    }

    private void SpawnObject()
    {
        // Instantiate the object with the predetermined rotation
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.Euler(spawnRotation));
        currentSpawnedObjects.Add(spawnedObject); // Add to the list of spawned objects
        currentTargetIndexes.Add(0); // Start at point A for the new object
    }

    private void MoveBetweenPoints(GameObject spawnedObject, int index)
    {
        // Get the current target point based on the target index
        int targetIndex = currentTargetIndexes[index];
        Transform targetPoint = targetIndex switch
        {
            0 => pointA,
            1 => pointB,
            2 => pointC,
            _ => pointA // Fallback to point A
        };

        // Move towards the target point
        spawnedObject.transform.position = Vector3.MoveTowards(spawnedObject.transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // Check if the object has reached the target point
        if (Vector3.Distance(spawnedObject.transform.position, targetPoint.position) < 0.1f)
        {
            // Move to the next target point
            currentTargetIndexes[index]++;

            // If we've reached the end of the points, destroy the object
            if (currentTargetIndexes[index] >= 3)
            {
                Destroy(spawnedObject); // Destroy the object at the end
                currentSpawnedObjects.RemoveAt(index); // Remove from the list
                currentTargetIndexes.RemoveAt(index); // Remove the target index
            }
        }
    }
}
